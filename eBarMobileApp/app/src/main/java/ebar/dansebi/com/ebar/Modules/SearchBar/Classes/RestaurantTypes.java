package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

/**
 * Created by sebas on 6/12/2018.
 */

public enum RestaurantTypes {
    Pizza("Pizzerie", 0),
    FastFood("Fast-food", 1),
    Traditional("Tradițional", 2),
    Cafe("Cafenea", 3),
    Pub("Pub", 4);


    public String restaurantTypeValue;
    public int idx;

    private RestaurantTypes(String restaurantTypeValue, int idx) {
        this.restaurantTypeValue = restaurantTypeValue;
        this.idx = idx;
    }

    public static String getValue(RestaurantTypes restaurantType) {
        return restaurantType.restaurantTypeValue;
    }

    public static RestaurantTypes getRestaurantTypeByStringValue(String stringValue) {
        return RestaurantTypes.valueOf(stringValue);
    }
}
