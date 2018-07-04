package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

import android.graphics.drawable.AnimationDrawable;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;

import ebar.dansebi.com.ebar.R;

/**
 * Created by sebas on 6/27/2018.
 */

public class BarsListRecyclerViewAdapter extends RecyclerView.Adapter {
    private final int VIEW_ITEM = 1;
    private final int VIEW_PROG = 0;

    private ArrayList<RestaurantDetails> restaurantDetailsArrayList;

    // The minimum amount of items to have below your current scroll position
// before loading more.
    private int visibleThreshold = 5;
    private int lastVisibleItem, totalItemCount;
    private boolean loading;
    private OnLoadMoreListener onLoadMoreListener;


    public BarsListRecyclerViewAdapter(ArrayList<RestaurantDetails> restaurantDetailsArrayList, RecyclerView recyclerView) {
        this.restaurantDetailsArrayList = restaurantDetailsArrayList;

        if (recyclerView.getLayoutManager() instanceof LinearLayoutManager) {

            final LinearLayoutManager linearLayoutManager = (LinearLayoutManager) recyclerView
                    .getLayoutManager();


            recyclerView
                    .addOnScrollListener(new RecyclerView.OnScrollListener() {
                        @Override
                        public void onScrolled(RecyclerView recyclerView,
                                               int dx, int dy) {
                            super.onScrolled(recyclerView, dx, dy);

                            totalItemCount = linearLayoutManager.getItemCount();
                            lastVisibleItem = linearLayoutManager
                                    .findLastVisibleItemPosition();
                            if (!loading
                                    && totalItemCount <= (lastVisibleItem + visibleThreshold)) {
                                // End has been reached
                                // Do something
                                if (onLoadMoreListener != null) {
                                    onLoadMoreListener.onLoadMore();
                                }
                                loading = true;
                            }
                        }
                    });
        }
    }

    @Override
    public int getItemViewType(int position) {
        return restaurantDetailsArrayList.get(position) != null ? VIEW_ITEM : VIEW_PROG;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent,
                                                      int viewType) {
        RecyclerView.ViewHolder vh;
        if (viewType == VIEW_ITEM) {
            View v = LayoutInflater.from(parent.getContext()).inflate(
                    R.layout.fragment_searchbar_barslist_bar_item, parent, false);

            vh = new RestaurantViewHolder(v);
        } else {
            View v = LayoutInflater.from(parent.getContext()).inflate(
                    R.layout.fragment_searchbar_barslist_bar_item_progress, parent, false);

            vh = new ProgressViewHolder(v);
        }
        return vh;
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        if (holder instanceof RestaurantViewHolder) {
            RestaurantDetails restaurantDetails = (RestaurantDetails) restaurantDetailsArrayList.get(position);
            //load picture
            DecodeBas64ToBitmapTask l_decodeBase64ToBmp = new DecodeBas64ToBitmapTask(((RestaurantViewHolder) holder).restaurantImage);
            l_decodeBase64ToBmp.execute(restaurantDetails);

            //set name
            ((RestaurantViewHolder) holder).restaurantName.setText(
                    restaurantDetails.getRestaurantName() != null ? restaurantDetails.getRestaurantName() : "Restaurant " + String.valueOf(position));

            ((RestaurantViewHolder) holder).restaurantType.setText(
                    restaurantDetails.getRestaurantType() != null ? restaurantDetails.getRestaurantType() : "Restaurant cu specific romanesc");

            ((RestaurantViewHolder) holder).restaurantReviews.setText(
                    position == 1 ? "1 recenzie" : String.valueOf(position) + " recenzii");

            ((RestaurantViewHolder) holder).restaurantAddress.setText(
                    restaurantDetails.getRestaurantAddress() != null ? restaurantDetails.getRestaurantAddress() : "Strada Traian, 13, Calafat, Dolj");

            ((RestaurantViewHolder) holder).restaurantDetails = restaurantDetails;

        } else {
            ((ProgressViewHolder) holder).progressBar.setBackgroundResource(R.drawable.spinner_anim);
            AnimationDrawable frameAnimation = (AnimationDrawable) ((ProgressViewHolder) holder).progressBar.getBackground();
            frameAnimation.start();
        }
    }

    public void setLoaded() {
        loading = false;
    }

    @Override
    public int getItemCount() {
        return restaurantDetailsArrayList.size();
    }

    public void setOnLoadMoreListener(OnLoadMoreListener onLoadMoreListener) {
        this.onLoadMoreListener = onLoadMoreListener;
    }


    public static class RestaurantViewHolder extends RecyclerView.ViewHolder {
        public ImageView restaurantImage;
        public TextView restaurantName;
        public TextView restaurantType;
        public TextView restaurantReviews;
        public TextView restaurantAddress;
        public RestaurantDetails restaurantDetails;

        public RestaurantViewHolder(View v) {
            super(v);
            restaurantImage = (ImageView) v.findViewById(R.id.fsb_bl_bi_BarImage);
            restaurantName = (TextView) v.findViewById(R.id.fsb_bl_bi_BarName);
            restaurantType = (TextView) v.findViewById(R.id.fsb_bl_bi_BarType);
            restaurantReviews = (TextView) v.findViewById(R.id.fsb_bl_bi_BarReviews);
            restaurantAddress = (TextView) v.findViewById(R.id.fsb_bl_bi_BarAddress);

            v.setOnClickListener(new View.OnClickListener() {

                @Override
                public void onClick(View v) {
                    Toast.makeText(v.getContext(),
                            "OnClick :" + restaurantDetails.getRestaurantName(),
                            Toast.LENGTH_SHORT).show();

                }
            });
        }
    }

    public static class ProgressViewHolder extends RecyclerView.ViewHolder {
        public ImageView progressBar;

        public ProgressViewHolder(View v) {
            super(v);
            progressBar = (ImageView) v.findViewById(R.id.fsblSpinner);
        }
    }
}
