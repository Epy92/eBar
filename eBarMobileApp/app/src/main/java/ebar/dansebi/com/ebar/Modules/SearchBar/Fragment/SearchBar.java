package ebar.dansebi.com.ebar.Modules.SearchBar.Fragment;

import android.content.Context;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.support.design.widget.CoordinatorLayout;
import android.support.v4.app.Fragment;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.widget.CardView;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.DisplayMetrics;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.text.Normalizer;
import java.util.ArrayList;

import ebar.dansebi.com.ebar.Activities.MainActivity;
import ebar.dansebi.com.ebar.Features.CommonFunctions;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.DistanceTypes;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.GetRestaurantsTask;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.OnTaskCompletedListener;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.RestaurantDetails;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.RestaurantType;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.SearchBarFilters;
import ebar.dansebi.com.ebar.R;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResponse;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultFunctions;


public class SearchBar extends Fragment implements OnTaskCompletedListener {

    private OnTaskCompletedListener m_onTaskCompletedListener;
    private CoordinatorLayout m_coordLayout;
    private Context m_context;
    private EditText m_etRestDescription;
    private TextView m_tvRestCateg, m_tvRestCounty, m_tvRestCity;
    private ImageView m_ivClearDescription, m_ivClearCategory, m_ivClearCounty, m_ivClearCity;
    private CheckBox m_cbUseMyLocation;
    private Spinner m_spDistance;
    private LinearLayout m_layoutSearchByGPS, m_layoutSearchByCity;
    private CardView m_getResultsBtn;
    private ArrayList<RestaurantType> m_selectedRestaurantTypes;
    private double m_longitude, m_latitude;

    public SearchBar() {
        // Required empty public constructor
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        m_onTaskCompletedListener = this;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View m_view = inflater.inflate(R.layout.fragment_searchbar, container, false);
        m_context = m_view.getContext();

        m_etRestDescription = (EditText) m_view.findViewById(R.id.fsbEtRestaurantDescription);
        m_tvRestCateg = (TextView) m_view.findViewById(R.id.fsbTvRestaurantCategory);
        m_tvRestCounty = (TextView) m_view.findViewById(R.id.fsbCounty);
        m_tvRestCity = (TextView) m_view.findViewById(R.id.fsbCity);
        m_ivClearDescription = (ImageView) m_view.findViewById(R.id.fsbIvRemoveDescription);
        m_ivClearCategory = (ImageView) m_view.findViewById(R.id.fsbIvRemoveCategory);
        m_ivClearCounty = (ImageView) m_view.findViewById(R.id.fsbIvRemoveCounty);
        m_ivClearCity = (ImageView) m_view.findViewById(R.id.fsbIvRemoveCity);
        m_cbUseMyLocation = (CheckBox) m_view.findViewById(R.id.fsbCbUseMyLocation);
        m_spDistance = (Spinner) m_view.findViewById(R.id.fsbDistance);
        m_layoutSearchByGPS = (LinearLayout) m_view.findViewById(R.id.fsbLlSearchLocationByGPS);
        m_layoutSearchByCity = (LinearLayout) m_view.findViewById(R.id.fsbLlSearchLocationByCountyAndCity);
        m_getResultsBtn = (CardView) m_view.findViewById(R.id.fsbCvGetResults);
        m_coordLayout = (CoordinatorLayout) m_view.findViewById(R.id.login_coordLayout);

        m_longitude = 0.0;
        m_latitude = 0.0;

        initRestaurantDescription();
        initClearButtons();
        initRestaurantCategories();
        initCounties();
        initCities();
        initUseMyLocation();
        initDistance();
        initGetResultsTasks();

        return m_view;
    }

    //region USE_MY_LOCATION
    private void initUseMyLocation() {
        m_cbUseMyLocation.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (!isChecked) {
                    m_layoutSearchByCity.setVisibility(isChecked ? View.GONE : View.VISIBLE);
                    m_layoutSearchByGPS.setVisibility(isChecked ? View.VISIBLE : View.GONE);
                    initLocationParams();
                } else {
                    if (((MainActivity) getActivity()).isLocationPermissionGranted()) {
                        useMyLocation();
                    } else {
                        m_cbUseMyLocation.setChecked(false);
                        ((MainActivity) getActivity()).askForLocationPermission();
                    }
                }
            }
        });
    }


    private void initLocationParams() {
        m_latitude = 0.0;
        m_longitude = 0.0;
    }

    public void setLocationParams(double latitude, double longitude) {
        m_cbUseMyLocation.setChecked(true);
        m_layoutSearchByCity.setVisibility(View.GONE);
        m_layoutSearchByGPS.setVisibility(View.VISIBLE);
        m_latitude = latitude;
        m_longitude = longitude;
    }

    public void useMyLocation() {
        if (((MainActivity) getActivity()).isLocationServiceTurnedOn()) {
            ((MainActivity) getActivity()).getDeviceLocation();
            //  boolean l_currentLocationRetrieved = ((MainActivity) getActivity()).getDeviceLocation();
//            if (l_currentLocationRetrieved) {
//                m_layoutSearchByCity.setVisibility(View.GONE);
//                m_layoutSearchByGPS.setVisibility(View.VISIBLE);
//            } else {
//                m_cbUseMyLocation.setChecked(false);
//                CommonFunctions.showMessage(
//                        m_context,
//                        CommonFunctions.getStringName("searchBarLocationPermissionNotGrantedTitle", m_context),
//                        CommonFunctions.getStringName("searchBarLocationNotRetrieved", m_context)
//                );
//            }
        } else {
            m_cbUseMyLocation.setChecked(false);
            CommonFunctions.showMessage(
                    m_context,
                    CommonFunctions.getStringName("searchBarLocationPermissionNotGrantedTitle", m_context),
                    CommonFunctions.getStringName("searchBarLocationServiceDisabled", m_context)
            );
        }
    }
    //endregion

    //region DESCRIPTION
    private void initRestaurantDescription() {
        m_etRestDescription.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                manageViewsButtonsAndFonts(m_etRestDescription);
            }

            @Override
            public void afterTextChanged(Editable s) {

            }
        });
    }
    //endregion

    //region DISTANCE
    private void initDistance() {
        String[] l_distanceTypes = new String[DistanceTypes.values().length];
        for (int i = 0; i < DistanceTypes.values().length; i++) {
            l_distanceTypes[i] = DistanceTypes.values()[i].Title;
        }
        ArrayAdapter<String> l_distanceAdapter = new ArrayAdapter<>(m_context, R.layout.fragment_searchbar_distance_row_template, l_distanceTypes);
        l_distanceAdapter.setDropDownViewResource(R.layout.fragment_searchbar_distance_dropdown_row_template);
        m_spDistance.setAdapter(l_distanceAdapter);
    }
    //endregion

    //region SET_FIELDS_CLEAR_BTN
    private void manageViewsButtonsAndFonts(View view) {
        if (view instanceof EditText) {
            EditText l_currentEt = (EditText) view;
            boolean l_isEmpty = l_currentEt.getText().toString().isEmpty();
            l_currentEt.setTypeface(l_isEmpty ? Typeface.DEFAULT : Typeface.DEFAULT_BOLD);
            setClearDescriptionBtnVisiblity(!l_isEmpty);
        } else {
            TextView l_currentTv = (TextView) view;
            boolean l_isEmpty = l_currentTv.getText().toString().isEmpty();
            l_currentTv.setTypeface(l_isEmpty ? Typeface.DEFAULT : Typeface.DEFAULT_BOLD);
            if (l_currentTv.equals(m_tvRestCateg)) {
                setClearCategoryBtnVisiblity(!l_isEmpty);
            } else if (l_currentTv.equals(m_tvRestCounty)) {
                setClearCountyBtnVisiblity(!l_isEmpty);
                if (l_isEmpty) {
                    m_tvRestCity.setText("");
                    manageViewsButtonsAndFonts(m_tvRestCity);
                }
            } else if (l_currentTv.equals(m_tvRestCity)) {
                setClearCityBtnVisiblity(!l_isEmpty);
            }
        }
    }
    //endregion

    //region CLEAR_BUTTONS
    private void initClearButtons() {
        initClearDescription();
        initClearCategory();
        initClearCounty();
        initClearCity();
    }

    private void initClearDescription() {
        m_ivClearDescription.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                m_etRestDescription.setText("");
                manageViewsButtonsAndFonts(m_etRestDescription);
            }
        });
    }

    private void initClearCategory() {
        m_ivClearCategory.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                m_tvRestCateg.setText("");
                for (RestaurantType l_rest :
                        m_selectedRestaurantTypes) {
                    l_rest.setSelected(false);
                }
                manageViewsButtonsAndFonts(m_tvRestCateg);
            }
        });
    }

    private void initClearCounty() {
        m_ivClearCounty.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                m_tvRestCounty.setText("");
                manageViewsButtonsAndFonts(m_tvRestCounty);
            }
        });
    }

    private void initClearCity() {
        m_ivClearCity.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                m_tvRestCity.setText("");
                manageViewsButtonsAndFonts(m_tvRestCity);
            }
        });
    }

    private void setClearDescriptionBtnVisiblity(boolean state) {
        m_ivClearDescription.setVisibility(state ? View.VISIBLE : View.GONE);
    }

    private void setClearCategoryBtnVisiblity(boolean state) {
        m_ivClearCategory.setVisibility(state ? View.VISIBLE : View.GONE);
    }

    private void setClearCountyBtnVisiblity(boolean state) {
        m_ivClearCounty.setVisibility(state ? View.VISIBLE : View.GONE);
    }

    private void setClearCityBtnVisiblity(boolean state) {
        m_ivClearCity.setVisibility(state ? View.VISIBLE : View.GONE);
    }
    //endregion

    //region LOCAL_TYPES_MGMT
    private void initRestaurantCategories() {
        m_tvRestCateg.setCompoundDrawablesWithIntrinsicBounds(0, 0, R.drawable.ic_down, 0);
        m_tvRestCateg.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                chooseLocalTypes();
            }
        });

        m_selectedRestaurantTypes = new ArrayList<>();
        for (int i = 0; i < getResources().getStringArray(R.array.restaurantTypes).length; i++) {
            RestaurantType l_restaurantType = new RestaurantType();
            l_restaurantType.setTitle(getResources().getStringArray(R.array.restaurantTypes)[i]);
            l_restaurantType.setIdx(i);
            m_selectedRestaurantTypes.add(l_restaurantType);
        }
    }

    private void chooseLocalTypes() {

        final ArrayList<RestaurantType> l_restaurantTypes = new ArrayList<>();
        for (RestaurantType l_restaurantType :
                m_selectedRestaurantTypes) {
            RestaurantType l_newRestaurantType = new RestaurantType();
            l_newRestaurantType.setTitle(l_restaurantType.getTitle());
            l_newRestaurantType.setIdx(l_restaurantType.getIdx());
            l_newRestaurantType.setSelected(l_restaurantType.isSelected());
            l_restaurantTypes.add(l_newRestaurantType);
        }

        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(m_context);
        LayoutInflater inflater = getActivity().getLayoutInflater();
        final View dialogView = inflater.inflate(R.layout.dialog_choose_list, null);
        dialogBuilder.setView(dialogView);
        final AlertDialog alertDialog = dialogBuilder.create();

        //initializare controale
        TextView l_tvTitle = (TextView) dialogView.findViewById(R.id.dclTitle);
        ListView l_lvRestaurantTypes = (ListView) dialogView.findViewById(R.id.dclList);
        CardView l_cvCancel = (CardView) dialogView.findViewById(R.id.dclCancel);
        CardView l_cvConfirm = (CardView) dialogView.findViewById(R.id.dclConfirm);

        l_tvTitle.setText(CommonFunctions.getStringName("chooseRestaurantType", m_context));

        l_cvCancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                alertDialog.dismiss();
            }
        });

        l_cvConfirm.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setSelectedRestaurantTypes(l_restaurantTypes);
                manageViewsButtonsAndFonts(m_tvRestCateg);
                alertDialog.dismiss();
            }
        });
        //initiliazare array adapter pentru tipuri de restaurant
        RestaurantTypesAdapter l_adapter = new RestaurantTypesAdapter(m_context, l_restaurantTypes);
        l_lvRestaurantTypes.setAdapter(l_adapter);

        WindowManager l_wm = getActivity().getWindowManager();
        DisplayMetrics l_dm = new DisplayMetrics();
        getActivity().getWindowManager().getDefaultDisplay().getMetrics(l_dm);

        alertDialog.show();
        alertDialog.getWindow().setDimAmount(0.8f);
        alertDialog.getWindow().setLayout((int) (l_dm.widthPixels * 0.8), (int) (l_dm.heightPixels * 0.5));
        alertDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
    }

    private void setSelectedRestaurantTypes(ArrayList<RestaurantType> selectedRestaurantTypes) {
        StringBuffer l_tvTitle = new StringBuffer();
        for (RestaurantType l_restaurantType :
                selectedRestaurantTypes) {
            m_selectedRestaurantTypes.get(l_restaurantType.getIdx()).setSelected(l_restaurantType.isSelected());
            if (l_restaurantType.isSelected()) {
                if (l_tvTitle.length() > 0) {
                    l_tvTitle.append(", ");
                }
                l_tvTitle.append(l_restaurantType.getTitle());
            }
        }
        m_tvRestCateg.setText(l_tvTitle.toString());
        if (l_tvTitle.length() == 0)
            m_tvRestCateg.setBackground(ContextCompat.getDrawable(m_context, R.drawable.custom_spinner_background_not_selected));
        else
            m_tvRestCateg.setBackground(ContextCompat.getDrawable(m_context, R.drawable.custom_spinner_background_selected));
    }

    @Override
    public void callbackUI(WsCallResult wsCallResult) {
        if (wsCallResult.getWsResponse() != null){
            ((MainActivity) getActivity()).setM_resRestaurantDetails((ArrayList<RestaurantDetails>)wsCallResult.getWsResponse());
            ((MainActivity) getActivity()).switchFragment(3);
        }else{
            WsResultFunctions.showConnectionErrorMessage(wsCallResult.getCallResult(), m_context, m_coordLayout);
        }

    }


    //region RESTAURANT_TYPES_ADAPTER
    class RestaurantTypesAdapter extends ArrayAdapter<RestaurantType> {
        ArrayList<RestaurantType> l_restTypes;

        // View lookup cache
        class ViewHolder {
            TextView title;
            CheckBox selected;
        }

        public RestaurantTypesAdapter(Context context, ArrayList<RestaurantType> restaurantTypes) {
            super(context, R.layout.fragment_searchbar_tv_cb_row_template, restaurantTypes);
            l_restTypes = restaurantTypes;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            // Get the data item for this position
            RestaurantType restaurantType = getItem(position);
            // Check if an existing view is being reused, otherwise inflate the view
            ViewHolder viewHolder; // view lookup cache stored in tag
            if (convertView == null) {
                // If there's no view to re-use, inflate a brand new view for row
                viewHolder = new ViewHolder();
                LayoutInflater inflater = LayoutInflater.from(getContext());
                convertView = inflater.inflate(R.layout.fragment_searchbar_tv_cb_row_template, parent, false);

                viewHolder.title = (TextView) convertView.findViewById(R.id.fsbsrtText);
                viewHolder.selected = (CheckBox) convertView.findViewById(R.id.fsbsrtCheckbx);
                // Cache the viewHolder object inside the fresh view
                convertView.setTag(viewHolder);
                convertView.setId(restaurantType.getIdx());
                convertView.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        l_restTypes.get(v.getId()).setSelected(!l_restTypes.get(v.getId()).isSelected());
                        ((CheckBox) ((RelativeLayout) v).getChildAt(1)).setChecked(l_restTypes.get(v.getId()).isSelected());
                    }
                });

            } else {
                // View is being recycled, retrieve the viewHolder object from tag
                viewHolder = (ViewHolder) convertView.getTag();
            }
            // Populate the data from the data object via the viewHolder object
            // into the template view.
            viewHolder.title.setText(restaurantType.getTitle());
            viewHolder.selected.setChecked(restaurantType.isSelected());
            // Return the completed view to render on screen
            return convertView;
        }
    }
    //endregion

    //endregion

    //region COUNTIES_MGMT
    private void initCounties() {
        m_tvRestCounty.setCompoundDrawablesWithIntrinsicBounds(0, 0, R.drawable.ic_down, 0);
        m_tvRestCounty.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                chooseCounty();
            }
        });
    }

    private void chooseCounty() {
        String[] l_countiesList = getResources().getStringArray(R.array.Counties);

        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(m_context);
        LayoutInflater inflater = getActivity().getLayoutInflater();
        final View dialogView = inflater.inflate(R.layout.dialog_choose_list, null);
        dialogBuilder.setView(dialogView);
        final AlertDialog alertDialog = dialogBuilder.create();

        TextView l_tvTitle = (TextView) dialogView.findViewById(R.id.dclTitle);
        ListView l_lvCounties = (ListView) dialogView.findViewById(R.id.dclList);
        CardView l_cvCancel = (CardView) dialogView.findViewById(R.id.dclCancel);
        CardView l_cvConfirm = (CardView) dialogView.findViewById(R.id.dclConfirm);
        l_cvCancel.setVisibility(View.GONE);
        l_cvConfirm.setVisibility(View.GONE);

        l_tvTitle.setText(CommonFunctions.getStringName("searchBarCounty", m_context));

        CommonAdapter l_adapter = new CommonAdapter(m_context, l_countiesList, alertDialog, m_tvRestCounty);
        l_lvCounties.setAdapter(l_adapter);

        DisplayMetrics l_dm = new DisplayMetrics();
        getActivity().getWindowManager().getDefaultDisplay().getMetrics(l_dm);

        alertDialog.show();
        alertDialog.getWindow().setDimAmount(0.8f);
        alertDialog.getWindow().setLayout((int) (l_dm.widthPixels * 0.8), (int) (l_dm.heightPixels * 0.7));
        alertDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
    }
    //endregion

    //region CITIES_MGMT
    private void initCities() {
        m_tvRestCity.setCompoundDrawablesWithIntrinsicBounds(0, 0, R.drawable.ic_down, 0);
        m_tvRestCity.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!m_tvRestCounty.getText().toString().isEmpty()) {
                    chooseCity();
                } else {
                    String l_title = CommonFunctions.getStringName("searchBarIncompleteFields", m_context);
                    String l_msg = CommonFunctions.getStringName("searchBarPleaseSelectCounty", m_context);
                    CommonFunctions.showMessage(m_context, l_title, l_msg);
                }
            }
        });
    }

    private void chooseCity() {

        String l_selectedCounty = m_tvRestCounty.getText().toString().replace("-", "").replace(" ", "");
        l_selectedCounty = removeDiactrics(l_selectedCounty);
        int l_countyId = getResources().getIdentifier(l_selectedCounty, "array", getContext().getPackageName());
        String[] l_citiesList = getResources().getStringArray(l_countyId);

        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(m_context);
        LayoutInflater inflater = getActivity().getLayoutInflater();
        final View dialogView = inflater.inflate(R.layout.dialog_choose_list, null);
        dialogBuilder.setView(dialogView);
        final AlertDialog alertDialog = dialogBuilder.create();

        TextView l_tvTitle = (TextView) dialogView.findViewById(R.id.dclTitle);
        ListView l_lvCities = (ListView) dialogView.findViewById(R.id.dclList);
        CardView l_cvCancel = (CardView) dialogView.findViewById(R.id.dclCancel);
        CardView l_cvConfirm = (CardView) dialogView.findViewById(R.id.dclConfirm);
        l_cvCancel.setVisibility(View.GONE);
        l_cvConfirm.setVisibility(View.GONE);

        l_tvTitle.setText(CommonFunctions.getStringName("searchBarCity", m_context));

        CommonAdapter l_adapter = new CommonAdapter(m_context, l_citiesList, alertDialog, m_tvRestCity);
        l_lvCities.setAdapter(l_adapter);

        DisplayMetrics l_dm = new DisplayMetrics();
        getActivity().getWindowManager().getDefaultDisplay().getMetrics(l_dm);

        alertDialog.show();
        alertDialog.getWindow().setDimAmount(0.8f);
        alertDialog.getWindow().setLayout((int) (l_dm.widthPixels * 0.8), (int) (l_dm.heightPixels * 0.7));
        alertDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
    }

    private String removeDiactrics(String value) {
        return Normalizer.normalize(value, Normalizer.Form.NFD)
                .replaceAll("\\p{InCombiningDiacriticalMarks}+", "");

    }


    //endregion

    //region COUNTIES_AND_CITIES_LIST_ADAPTER
    class CommonAdapter extends ArrayAdapter<String> {
        String[] l_list;
        AlertDialog l_dialog;
        TextView l_currentTv;

        // View lookup cache
        class ViewHolder {
            TextView title;
        }

        public CommonAdapter(Context context, String[] list, AlertDialog alertDialog, TextView textView) {
            super(context, R.layout.fragment_searchbar_tv_row_template, list);
            l_list = list;
            l_dialog = alertDialog;
            l_currentTv = textView;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            // Get the data item for this position
            String l_item = getItem(position);
            // Check if an existing view is being reused, otherwise inflate the view
            ViewHolder viewHolder; // view lookup cache stored in tag
            if (convertView == null) {
                // If there's no view to re-use, inflate a brand new view for row
                viewHolder = new ViewHolder();
                LayoutInflater inflater = LayoutInflater.from(getContext());
                convertView = inflater.inflate(R.layout.fragment_searchbar_tv_row_template, parent, false);

                viewHolder.title = (TextView) convertView.findViewById(R.id.fsbsrtText);
                // Cache the viewHolder object inside the fresh view
                convertView.setTag(viewHolder);
                convertView.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        String l_oldSelection = l_currentTv.getText().toString();
                        String l_selection = ((TextView) ((RelativeLayout) v).getChildAt(0)).getText().toString();
                        l_currentTv.setText(l_selection);
                        manageViewsButtonsAndFonts(l_currentTv);
                        if (l_currentTv == m_tvRestCounty && !l_oldSelection.equals(l_selection)) {
                            m_tvRestCity.setText("");
                            manageViewsButtonsAndFonts(m_tvRestCity);
                            chooseCity();
                        }

                        l_dialog.dismiss();
                    }
                });

            } else

            {
                // View is being recycled, retrieve the viewHolder object from tag
                viewHolder = (ViewHolder) convertView.getTag();
            }
            // Populate the data from the data object via the viewHolder object
            // into the template view.
            viewHolder.title.setText(l_item);
            // Return the completed view to render on screen
            return convertView;
        }
    }
    //endregion

    //region GET_RESULTS
    private void initGetResultsTasks() {
        m_getResultsBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SearchBarFilters l_filters = new SearchBarFilters();
//                l_filters.setKeyword(m_etRestDescription.getText().toString().trim());
//                for (RestaurantType l_resType:
//                     m_selectedRestaurantTypes) {
//                    if (l_resType.isSelected())
//                        l_filters.setTypeIDs(l_filters.getTypeIDs().isEmpty()
//                                ? String.valueOf(l_resType.getIdx()) : l_filters.getTypeIDs() + ";" + String.valueOf(l_resType.getIdx()));
//                }
//                l_filters.setCounty(m_tvRestCounty.getText().toString().trim());
//                l_filters.setLocation(m_tvRestCity.getText().toString().trim());
//                l_filters.setLongitude(String.valueOf(m_longitude));
//                l_filters.setLatitude(String.valueOf(m_latitude));
//                l_filters.setRangeKm(((DistanceTypes) m_spDistance.getSelectedItem()).Value);
                l_filters.setRecordsToSkip(0);
                GetRestaurantsTask l_getRestaurants = new GetRestaurantsTask(m_context, m_onTaskCompletedListener, l_filters);
                l_getRestaurants.execute();
            }
        });
    }
    //endregion
}
