package ebar.dansebi.com.ebar.Activities;

import android.Manifest;
import android.content.Context;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.location.places.Place;
import com.google.android.gms.location.places.PlaceLikelihood;
import com.google.android.gms.location.places.PlaceLikelihoodBuffer;
import com.google.android.gms.location.places.Places;

import java.util.ArrayList;
import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.TimeUnit;

import ebar.dansebi.com.ebar.Features.SessionParameters;
import ebar.dansebi.com.ebar.Objects.MenuProduct;
import ebar.dansebi.com.ebar.Objects.NearLocation;
import ebar.dansebi.com.ebar.R;
import ebar.dansebi.com.ebar.Tasks.GetBarMenu;

public class BarActivity extends AppCompatActivity implements GoogleApiClient.OnConnectionFailedListener, GoogleApiClient.ConnectionCallbacks {

    private GoogleApiClient mGoogleApiClient;
    private static final int PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION = 405;
    private boolean mLocationPermissionGranted = false;
    private boolean jobGetLocationStarted = false;
    private boolean jobGetLocationIsRunnig = false;
    private static Context context;
    private LocationManager gpslm;
    private Location location;
    private ArrayList<NearLocation> nearLocations;
    private TextView tvLocationText, tvLocationBarName;
    private static Button btnGetMenu;
    private static LinearLayout llBarMenu;
    private static LayoutInflater inflater;
    private Timer timer;

    public static TextView tvShoppingCartCounter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        context = this;
        inflater = this.getLayoutInflater();
        tvLocationText = (TextView) findViewById(R.id.tvLocation1);
        tvLocationBarName = (TextView) findViewById(R.id.tvLocation2);
        llBarMenu = (LinearLayout) findViewById(R.id.llBarMenu);


        btnGetMenu = (Button) findViewById(R.id.btnGetMenu);
        btnGetMenu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (SessionParameters.getBarMenu().size() > 0) {
                    showBarMenu();
                } else {
                    getBarMenuFromServer();
                }
            }
        });


        // mGoogleApiClient = new GoogleApiClient.Builder(this)
        //        .enableAutoManage(this, this)
        //         .addConnectionCallbacks(this)
        //         .addApi(LocationServices.API)
        //        .addApi(Places.GEO_DATA_API)
        ////        .addApi(Places.PLACE_DETECTION_API)
        //        .build();
        //mGoogleApiClient.connect();
    }

    private void setLocation() {
        getDeviceLocation();
        if (!jobGetLocationStarted) {
            timer = new Timer();
            jobGetLocationStarted = true;
            timer.scheduleAtFixedRate(new TimerTask() {

                synchronized public void run() {
                    if (!jobGetLocationIsRunnig) {
                        new Thread() {
                            @Override
                            public void run() {
                                getCurrentPlaceReport();
                            }
                        }.start();
                    }
                }

            }, TimeUnit.SECONDS.toMillis(1), TimeUnit.SECONDS.toMillis(5));
        }
    }

    private void getDeviceLocation() {

        if (ContextCompat.checkSelfPermission(this.getApplicationContext(),
                android.Manifest.permission.ACCESS_FINE_LOCATION)
                == PackageManager.PERMISSION_GRANTED) {

            gpslm = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

            try {
                LocationListener gpsLocationListener = new LocationListener() {
                    @Override
                    public void onLocationChanged(Location receivedLocation) {
                        location = receivedLocation;
                        Toast.makeText(context, "-gps location changed-", Toast.LENGTH_SHORT).show();

                    }

                    @Override
                    public void onStatusChanged(String provider, int status, Bundle extras) {

                    }

                    @Override
                    public void onProviderEnabled(String provider) {

                    }

                    @Override
                    public void onProviderDisabled(String provider) {

                    }
                };

                gpslm.requestLocationUpdates(LocationManager.GPS_PROVIDER, 100, 1F, gpsLocationListener);
                location = gpslm.getLastKnownLocation(LocationManager.GPS_PROVIDER);

            } catch (Exception e) {
                e.printStackTrace();
            }


            mLocationPermissionGranted = true;

            nearLocations = new ArrayList<NearLocation>();
        } else {
            ActivityCompat.requestPermissions(this,
                    new String[]{android.Manifest.permission.ACCESS_FINE_LOCATION},
                    PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION);
        }
    }

    private void getCurrentPlaceReport() {
        jobGetLocationIsRunnig = true;

        if (mLocationPermissionGranted) {
            if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                return;
            }

            final ArrayList<Integer> placeRestrictions = new ArrayList<Integer>();
            placeRestrictions.add(Place.TYPE_BAR);
            placeRestrictions.add(Place.TYPE_RESTAURANT);
            placeRestrictions.add(Place.TYPE_FOOD);
            placeRestrictions.add(Place.TYPE_CAFE);

            PendingResult<PlaceLikelihoodBuffer> result = Places.PlaceDetectionApi
                    .getCurrentPlace(mGoogleApiClient, null);

            result.setResultCallback(new ResultCallback<PlaceLikelihoodBuffer>() {

                @Override
                public void onResult(@NonNull PlaceLikelihoodBuffer likelyPlaces) {

                    try {
                        int i = 0;
                        nearLocations.clear();
                        for (PlaceLikelihood placeLikelihood : likelyPlaces) {
                            for (Integer placeType : placeLikelihood.getPlace().getPlaceTypes()) {
                                if (placeRestrictions.contains(placeType)) {

                                    Location barLocation = new Location("");
                                    barLocation.setLatitude(placeLikelihood.getPlace().getLatLng().latitude);
                                    barLocation.setLongitude(placeLikelihood.getPlace().getLatLng().longitude);

                                    NearLocation nl = new NearLocation((String) placeLikelihood.getPlace().getName(), (String) placeLikelihood.getPlace().getAddress(), location.distanceTo(barLocation));

                                    nearLocations.add(nl);
                                    if (nearLocations.size() == 10)
                                        break;
                                }
                            }
                            i++;
                        }
                        likelyPlaces.release();
                        if (nearLocations.size() > 0) {
                            NearLocation nearestLocation = null;
                            Float minDistance = 150f;

                            for (NearLocation nl : nearLocations
                                    ) {
                                if (nl.getDistance() < minDistance) {
                                    minDistance = nl.getDistance();
                                    nearestLocation = nl;
                                }
                            }

                            if (nearestLocation != null) {
                                if (tvLocationBarName.getVisibility() == View.INVISIBLE) {
                                    ProgressBar pdLoadingLocation = (ProgressBar) findViewById(R.id.pdLoadingLocation);
                                    pdLoadingLocation.setVisibility(View.INVISIBLE);
                                    tvLocationText.setVisibility(View.VISIBLE);
                                    tvLocationBarName.setVisibility(View.VISIBLE);
                                    btnGetMenu.setVisibility(View.VISIBLE);
                                }

                                tvLocationBarName.setText(nearestLocation.getName() + " (" + Integer.toString(Math.round(nearestLocation.getDistance())) + "m)");
                                SessionParameters.setCurrentBarname(nearestLocation.getName());
                                timer.cancel();
                                timer = null;
                            }
                        }
                        jobGetLocationIsRunnig = false;
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }


            });
        }
    }

    private void getBarMenuFromServer() {
        GetBarMenu getMenu = new GetBarMenu(context);
        getMenu.execute();
    }

    public static void showBarMenu() {
        Runnable showMenuThread = new Runnable() {
            @Override
            public void run() {
                String currProductCategory = "";
                String currProductType = "";
                llBarMenu.removeAllViews();
                for (final MenuProduct product : SessionParameters.getBarMenu()) {

                    if (!currProductCategory.matches(product.getProductCategory())) {

                        currProductCategory = product.getProductCategory();
                        currProductType = product.getProductType();

                        View rowHeaderTemplateProductCategory = inflater.inflate(R.layout.bar_menu_row_header_template_product_category, null);
                        View rowHeaderTemplateProductType = inflater.inflate(R.layout.bar_menu_row_header_template_product_type, null);

                        TextView prodCategory = (TextView) rowHeaderTemplateProductCategory.findViewById(R.id.productCategory);
                        TextView prodType = (TextView) rowHeaderTemplateProductType.findViewById(R.id.productType);

                        prodCategory.setText(currProductCategory);
                        prodType.setText(currProductType);

                        llBarMenu.addView(rowHeaderTemplateProductCategory);
                        llBarMenu.addView(rowHeaderTemplateProductType);

                    } else {

                        if (!currProductType.matches(product.getProductType())) {

                            currProductType = product.getProductType();
                            View rowHeaderTemplateProductType = inflater.inflate(R.layout.bar_menu_row_header_template_product_type, null);
                            TextView prodType = (TextView) rowHeaderTemplateProductType.findViewById(R.id.productType);

                            prodType.setText(currProductType);
                            llBarMenu.addView(rowHeaderTemplateProductType);

                        }
                    }


                    View rowTemplate = inflater.inflate(R.layout.bar_menu_row_template, null);
                    TextView prodName = (TextView) rowTemplate.findViewById(R.id.prodName);
                    TextView prodMadeOf = (TextView) rowTemplate.findViewById(R.id.prodMadeOf);
                    TextView prodPrice = (TextView) rowTemplate.findViewById(R.id.prodPrice);
                    TextView prodWeight = (TextView) rowTemplate.findViewById(R.id.prodWeight);
                    Button addToCart = (Button) rowTemplate.findViewById(R.id.btnAddToCart);

                    prodName.setText(product.getProductName());
                    prodMadeOf.setText(product.getProductMadeOF());
                    prodPrice.setText(product.getProductPrice().toString() + " " + product.getProductCurrency());
                    prodWeight.setText("(" + product.getProductWeight().toString() + " " + product.getProductUM() + ")");

                    addToCart.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            SessionParameters.shoppingCart.add(product);
                            if (tvShoppingCartCounter.getVisibility() == View.INVISIBLE)
                                tvShoppingCartCounter.setVisibility(View.VISIBLE);
                            tvShoppingCartCounter.setText(TextUtils.isEmpty(tvShoppingCartCounter.getText().toString().trim()) ? "1" : String.valueOf(Integer.valueOf(tvShoppingCartCounter.getText().toString().trim()) + 1));
                        }
                    });
                    llBarMenu.addView(rowTemplate);


                }

                btnGetMenu.setVisibility(View.INVISIBLE);
            }
        };
        showMenuThread.run();
    }


    @Override
    public void onBackPressed() {
//        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
//        if (drawer.isDrawerOpen(GravityCompat.START)) {
//            drawer.closeDrawer(GravityCompat.START);
//        } else {
//            super.onBackPressed();
//        }
    }


    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {

    }

    @Override
    public void onConnected(@Nullable Bundle bundle) {
        setLocation();
    }

    @Override
    public void onConnectionSuspended(int i) {

    }
}