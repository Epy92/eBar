package ebar.dansebi.com.ebar.Objects;

/**
 * Created by sebas on 8/1/2017.
 */

public class NearLocation {
    private String name, address;
    private Float distance;

    public NearLocation(String name, String address, Float distance) {
        this.name = name;
        this.address = address;
        this.distance = distance;
    }

    public Float getDistance() {
        return distance;
    }

    public void setDistance(Float distance) {
        this.distance = distance;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }
}
