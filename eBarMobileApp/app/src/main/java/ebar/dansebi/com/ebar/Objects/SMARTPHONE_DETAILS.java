package ebar.dansebi.com.ebar.Objects;

/**
 * Created by sebas on 7/27/2017.
 */

public class SMARTPHONE_DETAILS {
    private String UUID, DEVICE_TOKEN;

    public SMARTPHONE_DETAILS(){
        this.UUID = null;
        this.DEVICE_TOKEN = null;
    }

    public String getUUID() {
        return UUID;
    }

    public  void setUUID(String UUID) {
        this.UUID = UUID;
    }

    public String getDeviceToken() {
        return DEVICE_TOKEN;
    }

    public void setDeviceToken(String deviceToken) {
        this.DEVICE_TOKEN = deviceToken;
    }
}
