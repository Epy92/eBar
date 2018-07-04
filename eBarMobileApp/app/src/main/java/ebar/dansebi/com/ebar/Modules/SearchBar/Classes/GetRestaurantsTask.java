package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

import android.content.Context;
import android.os.AsyncTask;

import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashMap;

import ebar.dansebi.com.ebar.Features.EbarProgressDialog;
import ebar.dansebi.com.ebar.Services.CallWsHelper.GenericRunService;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceRequestTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;

/**
 * Created by sebas on 6/24/2018.
 */

public class GetRestaurantsTask extends AsyncTask<Void, Void, WsCallResult> {
    private Context m_context;
    public OnTaskCompletedListener onTaskCompletedListener;
    private EbarProgressDialog m_dialog;
    private SearchBarFilters m_filters;

    public GetRestaurantsTask(Context context, OnTaskCompletedListener onTaskCompletedListener, SearchBarFilters searchBarFilters) {
        m_context = context;
        this.onTaskCompletedListener = onTaskCompletedListener;
        m_filters = searchBarFilters;
    }

    @Override
    protected WsCallResult doInBackground(Void... voids) {
        WsCallResult m_wsCallResult = new WsCallResult();
        try {
            HashMap<String, String> l_params = new HashMap<>();
            l_params.put("keyword", "");
            l_params.put("location", "");
            l_params.put("county", "");
            l_params.put("typeIDs", "");
            l_params.put("lat", "");
            l_params.put("longitude", "");
            l_params.put("rangeKm", "0");
            l_params.put("nrOfRecordsToSkip", "0");

            GenericRunService l_grs = new GenericRunService();
            Type l_deserializedObjType = new TypeToken<ArrayList<RestaurantDetails>>() {
            }.getType();
            m_wsCallResult = l_grs.CallWebservice(WebserviceTypes.RestaurantService, WebserviceRequestTypes.GET,
                    "GetRestaurantsByParameters", l_params, m_context, l_deserializedObjType);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return m_wsCallResult;
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
        if (m_filters.getRecordsToSkip() == 0) {
            m_dialog = new EbarProgressDialog(m_context);
            m_dialog.show();
        }
    }

    @Override
    protected void onPostExecute(WsCallResult wsCallResult) {
        super.onPostExecute(wsCallResult);
        if (m_filters.getRecordsToSkip() == 0) {
            if (m_dialog.isShowing())
                m_dialog.dismiss();
        }
        onTaskCompletedListener.callbackUI(wsCallResult);
    }
}
