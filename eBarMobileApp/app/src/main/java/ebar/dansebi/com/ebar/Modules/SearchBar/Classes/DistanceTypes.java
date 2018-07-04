package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

/**
 * Created by sebas on 6/20/2018.
 */

public enum DistanceTypes {
    Five("+5 km", 5),
    Ten("+10 km", 10),
    Fifteen("+15 km", 15),
    Twenty("+20 km", 20);

    public String Title;
    public int Value;
    private DistanceTypes(String title, int value){
        this.Title = title;
        this.Value = value;
    }

    public static String getTitle(DistanceTypes distanceType){
        return distanceType.Title;
    }

    public static int getValue(DistanceTypes distanceType){
        return distanceType.Value;
    }

    public static DistanceTypes getExpenseTypeByStringValue(String stringValue){
        return DistanceTypes.valueOf(stringValue);
    }
}
