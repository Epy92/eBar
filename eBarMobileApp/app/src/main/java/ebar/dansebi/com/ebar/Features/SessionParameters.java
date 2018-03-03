package ebar.dansebi.com.ebar.Features;

import java.util.ArrayList;

import ebar.dansebi.com.ebar.Objects.MenuProduct;

/**
 * Created by sebas on 7/26/2017.
 */

public class SessionParameters {
    private static SQLiteDB myDB;
    private static ArrayList<MenuProduct> barMenu = new ArrayList<MenuProduct>();
    public static ArrayList<MenuProduct> shoppingCart = new ArrayList<MenuProduct>();
    private static String currentBarname;
    private static boolean sendDeviceTokenToServerIsRunning, timerForSendingDevTokenToServerIsRunning, getAndroidLocationGranted;


    public static SQLiteDB getMyDB() {
        return myDB;
    }

    public static void setMyDB(SQLiteDB _myDB) {
        myDB = _myDB;
    }


    public static ArrayList<MenuProduct> getBarMenu() {
        return barMenu;
    }

    public static void setBarMenu(ArrayList<MenuProduct> barMenu) {
        SessionParameters.barMenu = barMenu;
    }

    public static void addProductToBarMenu(MenuProduct menuProduct){
        barMenu.add(menuProduct);
    }

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


    public static ArrayList<MenuProduct> getShoppingCart() {
        return shoppingCart;
    }

    public static void setShoppingCart(ArrayList<MenuProduct> shoppingCart) {
        SessionParameters.shoppingCart = shoppingCart;
    }
}
