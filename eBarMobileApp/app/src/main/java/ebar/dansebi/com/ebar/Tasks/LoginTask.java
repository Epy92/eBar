package ebar.dansebi.com.ebar.Tasks;

import android.content.Context;
import android.os.AsyncTask;
import android.support.design.widget.CoordinatorLayout;

import org.json.JSONObject;

import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Fragments.Login;
import ebar.dansebi.com.ebar.Objects.CONNECTION_SETTINGS;

/**
 * Created by sebas on 3/1/2018.
 */

public class LoginTask extends AsyncTask<Void, Void, Integer> {
    private Context m_context;
    private CoordinatorLayout m_coordLayout;
    private String m_username, m_password;

    public LoginTask(Context context, CoordinatorLayout coordinatorLayout, String username, String password) {
        m_context = context;
        m_coordLayout = coordinatorLayout;
        m_username = username;
        m_password = password;
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
    }


    @Override
    protected Integer doInBackground(Void... voids) {
        int l_loginResult = -1;
        try {
            String l_loginResultStr;
            JSONObject l_loginJSON = new JSONObject();
            l_loginJSON.put("Username", m_username);
            l_loginJSON.put("UserPassword", m_password);
            CONNECTION_SETTINGS l_connSettings = MySharedPreferences.getConnectionSettings(m_context);
            URL l_url = new URL(l_connSettings.getWsAddress() + "UserLogin");
            HttpURLConnection l_connection = (HttpURLConnection) l_url.openConnection();
            l_connection.setRequestProperty("User-Agent", "");
            l_connection.setRequestProperty("Content-Type", "application/json");
            l_connection.setRequestMethod("POST");
            l_connection.setDoInput(true);
            l_connection.connect();

            DataOutputStream out = new DataOutputStream(l_connection.getOutputStream());
            out.write(l_loginJSON.toString().getBytes());
            out.flush();

            int l_responseCode = l_connection.getResponseCode();
            if (l_responseCode != 200) {
                return l_loginResult;
            }
            String message = l_connection.getResponseMessage();
            if (!message.equals("OK")) {
                return l_loginResult;
            }

            InputStream in1 = l_connection.getInputStream();
            StringBuffer sb = new StringBuffer();
            try {
                int chr;
                while ((chr = in1.read()) != -1) {
                    sb.append((char) chr);
                }
                l_loginResultStr = sb.toString();
                if (l_loginResultStr.contains("true")) {
                    l_loginResult = 1;
                } else {
                    l_loginResult = 0;
                }
            } finally {
                in1.close();
            }

        } catch (MalformedURLException e1) {
            e1.printStackTrace();
        } catch (IOException e1) {
            e1.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }

        return l_loginResult;
    }

    @Override
    protected void onPostExecute(Integer res) {
        super.onPostExecute(res);
        switch (res) {
            case -1:
                Login.showMessage("Cannot connect to server. Pleae verify the internet connection.", m_coordLayout);
                break;
            case 0:
                Login.showMessage("Invalid email or password.", m_coordLayout);
                break;
            case 1:
                Login.showMessage("ACCESS GRANTED", m_coordLayout);
                //do smtg
                break;
        }
    }

}
