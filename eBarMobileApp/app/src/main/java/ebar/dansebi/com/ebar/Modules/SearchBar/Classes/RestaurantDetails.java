package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

import android.graphics.Bitmap;

import com.google.gson.annotations.SerializedName;

/**
 * Created by sebas on 6/24/2018.
 */

public class RestaurantDetails {

    @SerializedName("Thumbnail")
    public Bitmap Thumbnail;
    @SerializedName("RestaurantId")
    public int RestaurantId;
    @SerializedName("RestaurantName")
    public String RestaurantName;
    @SerializedName("RestaurantCounty")
    public String RestaurantCounty;
    @SerializedName("RestaurantCity")
    public String RestaurantCity;
    @SerializedName("RestaurantAddress")
    public String RestaurantAddress;
    @SerializedName("RestaurantType")
    public String RestaurantType;
    @SerializedName("ThumbnailBase64String")
    public String ThumbnailBase64String;

    public RestaurantDetails(String restaurantName) {
        RestaurantName = restaurantName;
    }


    public int getRestaurantId() {
        return RestaurantId;
    }

    public void setRestaurantId(int restaurantId) {
        RestaurantId = restaurantId;
    }

    public String getRestaurantName() {
        return RestaurantName;
    }

    public void setRestaurantName(String restaurantName) {
        RestaurantName = restaurantName;
    }

    public String getRestaurantCity() {
        return RestaurantCity;
    }

    public void setRestaurantCity(String restaurantCity) {
        RestaurantCity = restaurantCity;
    }

    public String getRestaurantAddress() {
        return RestaurantAddress;
    }

    public void setRestaurantAddress(String restaurantAddress) {
        RestaurantAddress = restaurantAddress;
    }

    public String getThumbnailBase64String() {
        return ThumbnailBase64String;
    }

    public void setThumbnailBase64String(String thumbnailBase64String) {
        ThumbnailBase64String = thumbnailBase64String;
    }

    public Bitmap getThumbnail() {
        return Thumbnail;
    }

    public void setThumbnail(Bitmap thumbnail) {
        Thumbnail = thumbnail;
    }

    public String getRestaurantCounty() {
        return RestaurantCounty;
    }

    public void setRestaurantCounty(String restaurantCounty) {
        RestaurantCounty = restaurantCounty;
    }

    public String getRestaurantType() {
        return RestaurantType;
    }

    public void setRestaurantType(String restaurantType) {
        RestaurantType = restaurantType;
    }
}
