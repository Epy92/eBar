package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

/**
 * Created by sebas on 6/12/2018.
 */

public class RestaurantType {
    private String title;

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public int getIdx() {
        return idx;
    }

    public void setIdx(int idx) {
        this.idx = idx;
    }

    public boolean isSelected() {
        return selected;
    }

    public void setSelected(boolean selected) {
        this.selected = selected;
    }

    private int idx;
    private boolean selected;
}
