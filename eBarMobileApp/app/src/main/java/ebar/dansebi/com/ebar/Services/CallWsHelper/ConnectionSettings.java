package ebar.dansebi.com.ebar.Services.CallWsHelper;

/**
 * Created by sebas on 7/28/2017.
 */

public class ConnectionSettings {
    private String wsAddress, wsUsersSrv, wsRestaurantSrv, wsUsername, wsPassword;

    public ConnectionSettings() {
        this.wsAddress = null;
        this.wsUsername = null;
        this.wsPassword = null;
    }

    public String getWsAddress() {
        return wsAddress;
    }

    public void setWsAddress(String WEBSERVICE) {
        this.wsAddress = WEBSERVICE;
    }

    public String getWsUsername() {
        return wsUsername;
    }

    public void setWsUsername(String USERNAME) {
        this.wsUsername = USERNAME;
    }

    public String getWsPassword() {
        return wsPassword;
    }

    public void setWsPassword(String PASSWORD) {
        this.wsPassword = PASSWORD;
    }

    public String getWsUsersSrv() {
        return wsUsersSrv;
    }

    public void setWsUsersSrv(String wsUsersSrv) {
        this.wsUsersSrv = wsUsersSrv;
    }

    public String getWsRestaurantSrv() {
        return wsRestaurantSrv;
    }

    public void setWsRestaurantSrv(String wsRestaurantSrv) {
        this.wsRestaurantSrv = wsRestaurantSrv;
    }
}
