package ebar.dansebi.com.ebar.Features;

import java.util.ArrayList;

/**
 * Created by sebas on 7/26/2017.
 */

public class SessionParameters {
    private static String sessionKey;
    private static int languageCode;
    private static String currentBarname;
    private static boolean sendDeviceTokenToServerIsRunning, timerForSendingDevTokenToServerIsRunning, getAndroidLocationGranted;


    public static String getCurrentBarname() {
        return currentBarname;
    }

    public static void setCurrentBarname(String currentBarname) {
        SessionParameters.currentBarname = currentBarname;
    }

    public static boolean isSendDeviceTokenToServerIsRunning() {
        return sendDeviceTokenToServerIsRunning;
    }

    public static void setSendDeviceTokenToServerIsRunning(boolean sendDeviceTokenToServerIsRunning) {
        SessionParameters.sendDeviceTokenToServerIsRunning = sendDeviceTokenToServerIsRunning;
    }

    public static boolean isTimerForSendingDevTokenToServerIsRunning() {
        return timerForSendingDevTokenToServerIsRunning;
    }

    public static void setTimerForSendingDevTokenToServerIsRunning(boolean timerForSendingDevTokenToServerIsRunning) {
        SessionParameters.timerForSendingDevTokenToServerIsRunning = timerForSendingDevTokenToServerIsRunning;
    }

    public static boolean isGetAndroidLocationGranted() {
        return getAndroidLocationGranted;
    }

    public static void setGetAndroidLocationGranted(boolean getAndroidLocationGranted) {
        SessionParameters.getAndroidLocationGranted = getAndroidLocationGranted;
    }

    public static int getLanguageCode() {
        return languageCode;
    }

    public static void setLanguageCode(int languageCode) {
        SessionParameters.languageCode = languageCode;
    }

    public static String getSessionKey() {
        return sessionKey;
    }

    public static void setSessionKey(String sessionKey) {
        SessionParameters.sessionKey = sessionKey;
    }
}
