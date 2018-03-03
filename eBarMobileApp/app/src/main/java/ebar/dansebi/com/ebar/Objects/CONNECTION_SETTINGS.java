package ebar.dansebi.com.ebar.Objects;

/**
 * Created by sebas on 7/28/2017.
 */

public class CONNECTION_SETTINGS {
    private String wsAddress, wsUsername, wsPassword;

    public CONNECTION_SETTINGS() {
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

}
