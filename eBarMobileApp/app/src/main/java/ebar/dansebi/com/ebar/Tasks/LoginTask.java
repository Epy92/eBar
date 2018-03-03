package ebar.dansebi.com.ebar.Tasks;

import android.content.Context;
import android.os.AsyncTask;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Objects.CONNECTION_SETTINGS;

/**
 * Created by sebas on 3/1/2018.
 */

public class LoginTask extends AsyncTask {
    private Context m_context;
    private String m_username, m_password;

    public LoginTask(Context context, String username, String password) {
        m_context = context;
        m_username = username;
        m_password = password;
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
    }

    @Override
    protected Object doInBackground(Object[] objects) {
        try {
            StringBuffer sb = new StringBuffer("");
            JSONObject json;
            CONNECTION_SETTINGS l_connSettings = MySharedPreferences.getConnectionSettings(m_context);
            URL l_url = new URL(l_connSettings.getWsAddress() + "UserLogin" + "/" + m_username + "/" + m_password);
            HttpURLConnection l_connection = (HttpURLConnection) l_url.openConnection();
            l_connection.setRequestProperty("User-Agent", "");
            l_connection.setRequestMethod("GET");
            l_connection.setDoInput(true);
            l_connection.connect();


            InputStream inputStream = l_connection.getInputStream();

            BufferedReader rd = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while ((line = rd.readLine()) != null) {
                sb.append(line);
            }
            String response = sb.toString();
        } catch (MalformedURLException e1) {
            e1.printStackTrace();
        } catch (IOException e1) {
            e1.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    protected void onPostExecute(Object o) {
        super.onPostExecute(o);
    }
}
