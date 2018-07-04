package ebar.dansebi.com.ebar.Services.CallWsHelper;

import android.content.Context;

import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonParser;
import com.google.gson.stream.JsonReader;

import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.lang.reflect.Type;
import java.net.ConnectException;
import java.net.HttpURLConnection;
import java.net.SocketTimeoutException;
import java.net.URL;
import java.util.HashMap;

import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Features.SessionParameters;


/**
 * Created by sebas on 3/13/2018.
 */

public class GenericRunService {

    private WsCallResult CallWebServicePostMethod(WebserviceTypes wsType, WebserviceRequestTypes requestType, String methodName, String jsonToSend, Context context) {
        WsCallResult m_wsCallResult = new WsCallResult();
        try {
            ConnectionSettings l_connSettings = MySharedPreferences.getConnectionSettings(context);
            String l_serviceName = getServiceName(wsType, l_connSettings);
            URL l_url = new URL(l_connSettings.getWsAddress() + l_serviceName + methodName);
            HttpURLConnection l_connection = (HttpURLConnection) l_url.openConnection();
            l_connection.setRequestProperty("User-Agent", "");
            l_connection.setRequestProperty("Content-Type", "application/json");
            l_connection.setRequestMethod("POST");
            l_connection.setDoInput(true);
            l_connection.setConnectTimeout(5000);
            l_connection.connect();
            DataOutputStream out = new DataOutputStream(l_connection.getOutputStream());
            out.write(jsonToSend.getBytes());
            out.flush();
            int l_responseCode = l_connection.getResponseCode();
            if (l_responseCode != 200) {
                return m_wsCallResult;
            }
            String message = l_connection.getResponseMessage();
            if (!message.equals("OK")) {
                return m_wsCallResult;
            }
            JsonReader reader = new JsonReader(new InputStreamReader(l_connection.getInputStream()));
            JsonParser parser = new JsonParser();
            JsonElement jsonResponse = parser.parse(reader);

            m_wsCallResult.setWsResponse(new Gson().fromJson(jsonResponse.getAsString(), WsResponse.class));
        } catch (ConnectException e) {
            m_wsCallResult.setCallResult(-3);
            e.printStackTrace();
        } catch (SocketTimeoutException e) {
            m_wsCallResult.setCallResult(-2);
            e.printStackTrace();
        } catch (Exception e) {
            m_wsCallResult.setCallResult(-1);
            e.printStackTrace();
        }

        return m_wsCallResult;
    }

    public WsCallResult CallWebservice(WebserviceTypes wsType, WebserviceRequestTypes requestType, String methodName,
                                       HashMap<String, String> params, Context context, Type deserializedObjType) {
        WsCallResult m_wsCallResult = new WsCallResult();
        try {
            ConnectionSettings l_connSettings = MySharedPreferences.getConnectionSettings(context);
            String l_serviceName = getServiceName(wsType, l_connSettings);
            URL l_url = null;
            switch (requestType) {
                case POST:
                    l_url = new URL(l_connSettings.getWsAddress() + l_serviceName + methodName);
                    break;
                case GET:
                    boolean l_firstParamAdded = false;
                    StringBuffer l_urlStr = new StringBuffer();
                    l_urlStr.append(l_connSettings.getWsAddress() + l_serviceName + methodName);
                    for (HashMap.Entry<String, String> entry :
                            params.entrySet()) {
                        l_urlStr.append(l_firstParamAdded ? "&" : "?");
                        l_urlStr.append(entry.getKey());
                        l_urlStr.append("=");
                        l_urlStr.append(entry.getValue().trim());
                        l_firstParamAdded = true;
                    }
                    l_url = new URL(l_urlStr.toString());
                    break;
            }

            HttpURLConnection l_connection = (HttpURLConnection) l_url.openConnection();
            l_connection.setRequestProperty("User-Agent", "");
            l_connection.setRequestProperty("Content-Type", "application/json");
            l_connection.setRequestMethod(requestType.name().toString().toUpperCase());
            if (SessionParameters.getSessionKey() != null) {
                l_connection.setRequestProperty("Session_ID", SessionParameters.getSessionKey());
            }
            l_connection.setDoInput(true);
            l_connection.setConnectTimeout(5000);
            l_connection.connect();

            switch (requestType) {
                case POST:
                    DataOutputStream out = new DataOutputStream(l_connection.getOutputStream());
                    out.write(params.get("jsonData").getBytes());
                    out.flush();
                    break;
            }

            int l_responseCode = l_connection.getResponseCode();
            if (l_responseCode != 200) {
                return m_wsCallResult;
            }
            String message = l_connection.getResponseMessage();
            if (!message.equals("OK")) {
                return m_wsCallResult;
            }
            JsonReader reader = new JsonReader(new InputStreamReader(l_connection.getInputStream()));
            JsonParser parser = new JsonParser();
            JsonElement jsonResponse = parser.parse(reader);
            m_wsCallResult.setWsResponse(new Gson().fromJson(jsonResponse.getAsString(), deserializedObjType));

        } catch (ConnectException e) {
            m_wsCallResult.setCallResult(-3);
            e.printStackTrace();
        } catch (SocketTimeoutException e) {
            m_wsCallResult.setCallResult(-2);
            e.printStackTrace();
        } catch (Exception e) {
            m_wsCallResult.setCallResult(-1);
            e.printStackTrace();
        }

        return m_wsCallResult;
    }

    private String getServiceName(WebserviceTypes wsType, ConnectionSettings connectionSettings) {
        String l_servName = "";
        switch (wsType) {
            case UsersService:
                l_servName = connectionSettings.getWsUsersSrv();
                break;
            case RestaurantService:
                l_servName = connectionSettings.getWsRestaurantSrv();
                break;
        }
        return l_servName;
    }
}
