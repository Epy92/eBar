package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

/**
 * Created by sebas on 6/27/2018.
 */

public class SearchBarFilters {
    public String Keyword;
    public String Location;
    public String County;
    public String TypeIDs;
    public String Latitude;
    public String Longitude;
    public int RangeKm;
    public int RecordsToSkip;

    public SearchBarFilters() {
        Keyword = "";
        Location = "";
        County = "";
        TypeIDs = "";
        Latitude = "";
        Longitude = "";
        RangeKm = 0;
        RecordsToSkip = 0;
    }

    public String getKeyword() {
        return Keyword;
    }

    public void setKeyword(String keyword) {
        Keyword = keyword;
    }

    public String getLocation() {
        return Location;
    }

    public void setLocation(String location) {
        Location = location;
    }

    public String getCounty() {
        return County;
    }

    public void setCounty(String county) {
        County = county;
    }

    public String getTypeIDs() {
        return TypeIDs;
    }

    public void setTypeIDs(String typeIDs) {
        TypeIDs = typeIDs;
    }

    public String getLatitude() {
        return Latitude;
    }

    public void setLatitude(String latitude) {
        Latitude = latitude;
    }

    public String getLongitude() {
        return Longitude;
    }

    public void setLongitude(String longitude) {
        Longitude = longitude;
    }

    public int getRangeKm() {
        return RangeKm;
    }

    public void setRangeKm(int rangeKm) {
        RangeKm = rangeKm;
    }

    public int getRecordsToSkip() {
        return RecordsToSkip;
    }

    public void setRecordsToSkip(int recordsToSkip) {
        RecordsToSkip = recordsToSkip;
    }
}
