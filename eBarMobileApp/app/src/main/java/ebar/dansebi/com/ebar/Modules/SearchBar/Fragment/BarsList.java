package ebar.dansebi.com.ebar.Modules.SearchBar.Fragment;

import android.content.Context;
import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.Toast;

import java.util.ArrayList;

import ebar.dansebi.com.ebar.Activities.MainActivity;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.BarsListRecyclerViewAdapter;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.EndlessRecyclerOnScrollListener;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.GetRestaurantsTask;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.OnLoadMoreListener;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.OnTaskCompletedListener;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.RestaurantDetails;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.SearchBarFilters;
import ebar.dansebi.com.ebar.R;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultFunctions;


public class BarsList extends Fragment implements OnTaskCompletedListener {

    private OnTaskCompletedListener mOnTaskCompletedListener;
    private Context m_context;
    private RecyclerView m_barsListRecyclerView;
    private ArrayList<RestaurantDetails> m_restaurants;
    private BarsListRecyclerViewAdapter m_restaurantsAdapter;
    private ImageView m_spinner;
    Handler handler;

    public BarsList() {
        // Required empty public constructor
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        m_context = getContext();
        mOnTaskCompletedListener = this;
        handler = new Handler();
        View m_view = inflater.inflate(R.layout.fragment_searchbar_barslist, container, false);
        m_barsListRecyclerView = (RecyclerView) m_view.findViewById(R.id.fsblRecyclerList);
        m_spinner = (ImageView) m_view.findViewById(R.id.fsblSpinner);

        m_restaurants = ((MainActivity) getActivity()).getM_resRestaurantDetails();

        LinearLayoutManager linearLayoutManager = new LinearLayoutManager(m_context);
        linearLayoutManager.setOrientation(LinearLayoutManager.VERTICAL);
        m_barsListRecyclerView.setLayoutManager(linearLayoutManager);
        m_restaurantsAdapter = new BarsListRecyclerViewAdapter(m_restaurants, m_barsListRecyclerView);
        m_barsListRecyclerView.setAdapter(m_restaurantsAdapter);
        m_restaurantsAdapter.setOnLoadMoreListener(new OnLoadMoreListener() {
            @Override
            public void onLoadMore() {
                //add null , so the adapter will check view_type and show progress bar at bottom
                m_restaurants.add(null);
                m_restaurantsAdapter.notifyItemInserted(m_restaurants.size() - 1);

                handler.postDelayed(new Runnable() {
                    @Override
                    public void run() {

//                        //   remove progress item
//                        m_restaurants.remove(m_restaurants.size() - 1);
//                        m_restaurantsAdapter.notifyItemRemoved(m_restaurants.size());
//                        m_restaurants.addAll(m_restaurants);
//                        m_restaurantsAdapter.notifyDataSetChanged();
//                        m_restaurantsAdapter.setLoaded();
                        SearchBarFilters filter = new SearchBarFilters();
                        filter.setRecordsToSkip(m_restaurants.size());
                        GetRestaurantsTask l_getRestaurants = new GetRestaurantsTask(m_context, mOnTaskCompletedListener, filter);
                        l_getRestaurants.execute();
                        //or you can add all at once but do not forget to call mAdapter.notifyDataSetChanged();
                    }
                }, 500);

            }
        });

        return m_view;

    }

    @Override
    public void callbackUI(WsCallResult wsCallResult) {
        m_restaurants.remove(m_restaurants.size() - 1);
        m_restaurantsAdapter.notifyItemRemoved(m_restaurants.size());
//        m_restaurants.addAll((ArrayList<RestaurantDetails>)wsCallResult.getWsResponse());
//        m_restaurantsAdapter.notifyDataSetChanged();
//        m_restaurantsAdapter.setLoaded();
        if (wsCallResult.getWsResponse() != null){
            for (RestaurantDetails l_resDetails:
                    (ArrayList<RestaurantDetails>)wsCallResult.getWsResponse()) {
                m_restaurants.add(l_resDetails);
                m_restaurantsAdapter.notifyItemInserted(m_restaurants.size());
            }

            Toast.makeText(m_context, String.valueOf(m_restaurants.size()), Toast.LENGTH_SHORT).show();

        }
        m_restaurantsAdapter.setLoaded();
        //else{
//            WsResultFunctions.showConnectionErrorMessage(wsCallResult.getCallResult(), m_context, m_coordLayout);
//        }
//
//
//        //add items one by one
//        int start = m_restaurants.size();
//        int end = start + 20;
//
//        for (int i = start + 1; i <= end; i++) {
//            m_restaurants.add(new RestaurantDetails("R" + String.valueOf(i)));
//            m_restaurantsAdapter.notifyItemInserted(m_restaurants.size());
//        }
//        m_restaurantsAdapter.setLoaded();
    }
}
