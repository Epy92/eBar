package ebar.dansebi.com.ebar.Features;

import android.content.Context;
import android.content.SharedPreferences;

import java.util.Timer;
import java.util.TimerTask;
import java.util.UUID;
import java.util.concurrent.TimeUnit;

import ebar.dansebi.com.ebar.Services.CallWsHelper.ConnectionSettings;
import ebar.dansebi.com.ebar.Objects.SmartphoneDetails;
import ebar.dansebi.com.ebar.Objects.User;

/**
 * Created by sebas on 7/27/2017.
 */

public class MySharedPreferences {

    public static SmartphoneDetails getSmartphoneDetails(Context m_context) {
        SmartphoneDetails sd = new SmartphoneDetails();
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
                                //   SendToken login = new LoginTask(StartActivity.context, sharedpreferences.getString("DEVICE_TOKEN", null), sharedpreferences.getString("UUID", null), timer);
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
        SharedPreferences mySharedPreferences = m_context.getSharedPreferences("ConnectionSettings", Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = mySharedPreferences.edit();

        try {
            editor.putString("WEBSERVICE_ADDRESS", "http://192.168.100.107/eBarWS/api/");
            editor.putString("USER_SERVICE", "UserSession/");
            editor.putString("RESTAURANT_SERVICE", "Restaurant/");
            editor.putString("USERNAME", "admin");
            editor.putString("PASSWORD", "admin");

            editor.commit();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static ConnectionSettings getConnectionSettings(Context m_context) {
        ConnectionSettings connectionSettings = new ConnectionSettings();
        SharedPreferences mySharedPreferences = m_context.getSharedPreferences("ConnectionSettings", Context.MODE_PRIVATE);
        try {
            if (mySharedPreferences.getString("WEBSERVICE_ADDRESS", null) == null)
                setConnectionSettings(m_context);

            connectionSettings.setWsAddress(mySharedPreferences.getString("WEBSERVICE_ADDRESS", null));
            connectionSettings.setWsUsersSrv(mySharedPreferences.getString("USER_SERVICE", null));
            connectionSettings.setWsRestaurantSrv(mySharedPreferences.getString("RESTAURANT_SERVICE", null));
            connectionSettings.setWsUsername(mySharedPreferences.getString("USERNAME", null));
            connectionSettings.setWsPassword(mySharedPreferences.getString("PASSWORD", null));
        } catch (Exception e) {
            e.printStackTrace();
        }

        return connectionSettings;
    }

    public static boolean isLanguageSet(Context m_context) {
        SharedPreferences m_langPref = m_context.getSharedPreferences("LanguageSettings", Context.MODE_PRIVATE);
        try {
            if (m_langPref.getString("LANGUAGE", null) == null) {
                return false;
            } else {
                return true;
            }
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }

    public static void setDefaultLanguage(Context m_context) {
        SharedPreferences l_langPref = m_context.getSharedPreferences("LanguageSettings", Context.MODE_PRIVATE);
        SharedPreferences.Editor l_langEditor = l_langPref.edit();
        String l_prefLang = "ro";
        try {
            l_prefLang = m_context.getResources().getConfiguration().locale.getLanguage();
            l_langEditor.putString("LANGUAGE", l_prefLang);
            l_langEditor.commit();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static String getDefaultLanguage(Context m_context) {
        SharedPreferences l_langPref = m_context.getSharedPreferences("LanguageSettings", Context.MODE_PRIVATE);
        try {
            return l_langPref.getString("LANGUAGE", null);
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    public static User getLoggedInUser(Context context) {
        User l_user = new User();
        SharedPreferences l_sharedPref = context.getSharedPreferences("LOGGEDINUSER", Context.MODE_PRIVATE);
        try {
            if (l_sharedPref.getString("USERNAME", null) == null) {
                return null;
            }
            l_user.setUsername(l_sharedPref.getString("USERNAME", null));
            l_user.setUserPassword(l_sharedPref.getString("PASSWORD", null));
            return l_user;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    public static void setLoggedInUser(User user, Context context) {
        SharedPreferences l_sharedPref = context.getSharedPreferences("LOGGEDINUSER", Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = l_sharedPref.edit();

        try {
            editor.putString("USERNAME", user.getUsername());
            editor.putString("PASSWORD", user.getUserPassword());

            editor.commit();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

}
