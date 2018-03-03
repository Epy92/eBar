package ebar.dansebi.com.ebar.Features;

import android.content.Context;
import android.content.SharedPreferences;

import java.util.Timer;
import java.util.TimerTask;
import java.util.UUID;
import java.util.concurrent.TimeUnit;

import ebar.dansebi.com.ebar.Objects.CONNECTION_SETTINGS;
import ebar.dansebi.com.ebar.Objects.SMARTPHONE_DETAILS;

/**
 * Created by sebas on 7/27/2017.
 */

public class MySharedPreferences {

    public static SMARTPHONE_DETAILS getSmartphoneDetails(Context m_context) {
        SMARTPHONE_DETAILS sd = new SMARTPHONE_DETAILS();
        SharedPreferences sharedpreferences = m_context.getSharedPreferences("PUSH_NOTIFICATIONS", Context.MODE_PRIVATE);
        try {
            sd.setDeviceToken(sharedpreferences.getString("DEVICE_TOKEN", null));
            sd.setUUID(sharedpreferences.getString("UUID", null));
        } catch (Exception e) {
            e.printStackTrace();
        }
        return sd;
    }


    public static void setSmartphoneDetails(Context m_context, String m_devToken) {

        final SharedPreferences sharedpreferences = m_context.getSharedPreferences("PUSH_NOTIFICATIONS", Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedpreferences.edit();

        try {
            editor.putString("DEVICE_TOKEN", m_devToken);
        } catch (Exception e) {
            e.printStackTrace();
            editor.putString("DEVICE_TOKEN", null);
        }

        try {
            if (sharedpreferences.getString("UUID", null) == null)
                editor.putString("UUID", UUID.randomUUID().toString());
        } catch (Exception e) {
            e.printStackTrace();
            editor.putString("UUID", null);
        }

        try {
            editor.putBoolean("SENT_TO_SERVER", false);
        } catch (Exception e) {
            e.printStackTrace();
            editor.putBoolean("SENT_TO_SERVER", false);
        }

        editor.commit();

        if (!SessionParameters.isTimerForSendingDevTokenToServerIsRunning()) {
            SessionParameters.setTimerForSendingDevTokenToServerIsRunning(true);
            final Timer timer = new Timer();
            timer.scheduleAtFixedRate(new TimerTask() {
                synchronized public void run() {
                    if (!SessionParameters.isSendDeviceTokenToServerIsRunning()) {
                        new Thread() {
                            @Override
                            public void run() {
                             //   SendToken login = new LoginTask(Start.context, sharedpreferences.getString("DEVICE_TOKEN", null), sharedpreferences.getString("UUID", null), timer);
                           //     login.execute();
                            }
                        }.start();
                    }
                }
            }, TimeUnit.SECONDS.toMillis(1), TimeUnit.SECONDS.toMillis(5));
        }

    }


    public static Boolean confirmDeviceTokenSentToServer(Context m_context, Boolean state) {
        SharedPreferences sharedPreferences = m_context.getSharedPreferences("PUSH_NOTIFICATIONS", Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        boolean updated = true;
        try {
            editor.putBoolean("SENT_TO_SERVER", state);
            editor.commit();
        } catch (Exception e) {
            e.printStackTrace();
            updated = false;
        }
        return updated;
    }

    public static Boolean isDevTokenSentToServer(Context m_context) {
        SharedPreferences sharedPreferences = m_context.getSharedPreferences("PUSH_NOTIFICATIONS", Context.MODE_PRIVATE);
        boolean isSent = false;
        try {
            isSent = Boolean.valueOf(sharedPreferences.getBoolean("SENT_TO_SERVER", false));
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
        return isSent;
    }

    public static void setConnectionSettings(Context m_context) {
        SharedPreferences mySharedPreferences = m_context.getSharedPreferences("CONNECTION_SETTINGS", Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = mySharedPreferences.edit();

        try {
            editor.putString("WEBSERVICE", "http://192.168.100.105/eBarService/ServiceEbar.svc/");
            editor.putString("USERNAME", "admin");
            editor.putString("PASSWORD", "admin");

            editor.commit();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static CONNECTION_SETTINGS getConnectionSettings(Context m_context) {
        CONNECTION_SETTINGS connection_settings = new CONNECTION_SETTINGS();
        SharedPreferences mySharedPreferences = m_context.getSharedPreferences("CONNECTION_SETTINGS", Context.MODE_PRIVATE);
        try {
            if (mySharedPreferences.getString("WEBSERVICE", null) == null)
                setConnectionSettings(m_context);

            connection_settings.setWsAddress(mySharedPreferences.getString("WEBSERVICE", null));
            connection_settings.setWsUsername(mySharedPreferences.getString("USERNAME", null));
            connection_settings.setWsPassword(mySharedPreferences.getString("PASSWORD", null));
        } catch (Exception e) {
            e.printStackTrace();
        }

        return connection_settings;
    }


}
