package ebar.dansebi.com.ebar.Activities;

import android.Manifest;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Build;
import android.os.Bundle;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.NavigationView;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.FragmentManager;
import android.support.v4.content.ContextCompat;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.FrameLayout;

import java.util.ArrayList;

import ebar.dansebi.com.ebar.Features.CommonFunctions;
import ebar.dansebi.com.ebar.Modules.MainMenu.AppMenu;
import ebar.dansebi.com.ebar.Modules.SearchBar.Classes.RestaurantDetails;
import ebar.dansebi.com.ebar.Modules.SearchBar.Fragment.BarsList;
import ebar.dansebi.com.ebar.Modules.SearchBar.Fragment.SearchBar;
import ebar.dansebi.com.ebar.R;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    FrameLayout m_fragmentHolder;
    private final int LOCATION_PERMISSION_CODE = 102;
    private LocationManager m_locMgrGPS, m_locMgrNET;
    private static FragmentManager m_fragmentManager;
    private static Activity m_activity;
    private static CoordinatorLayout m_coordLayout;
    private ArrayList<RestaurantDetails> m_resRestaurantDetails;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

        m_fragmentHolder = (FrameLayout) findViewById(R.id.flFragmentHolder);
        m_fragmentManager = this.getSupportFragmentManager();
        m_activity = this;
        m_coordLayout = (CoordinatorLayout) findViewById(R.id.mainBotCl);
        switchFragment(1);//enter main menu form
    }

    public void switchFragment(int code) {
        switch (code) {
            case 1:
                m_fragmentManager.beginTransaction().replace(R.id.mainHolder, new AppMenu()).commit();
                break;
            case 2:
                m_fragmentManager.beginTransaction().replace(R.id.mainHolder, new SearchBar()).commit();
                break;
            case 3:
                m_fragmentManager.beginTransaction().replace(R.id.mainHolder, new BarsList()).commit();
                break;
        }
    }

    public boolean isLocationPermissionGranted() {
        if (ContextCompat.checkSelfPermission(m_activity.getApplicationContext(),
                Manifest.permission.ACCESS_FINE_LOCATION)
                == PackageManager.PERMISSION_GRANTED) {

            return true;
        } else {
            return false;
        }
    }

    public void loadFragmentBarsList(ArrayList<RestaurantDetails> restaurantDetailsArrayList){

    }

    @SuppressLint("MissingPermission")
    public void getDeviceLocation() {

        m_locMgrGPS = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
        m_locMgrNET = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

        final LocationListener l_netLocationListener = new LocationListener() {
            @Override
            public void onLocationChanged(Location location) {
                setLocationParamsInSearchBar(location, this, m_locMgrNET);
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
        m_locMgrNET.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 50, l_netLocationListener);

        Location l_lastGpsLocation = m_locMgrGPS.getLastKnownLocation(LocationManager.GPS_PROVIDER);
        Location l_lastNetLocation = m_locMgrNET.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
        if (l_lastGpsLocation != null) {
            setLocationParamsInSearchBar(l_lastGpsLocation, l_netLocationListener, m_locMgrNET);
        } else if (l_lastNetLocation != null) {
            setLocationParamsInSearchBar(l_lastNetLocation, l_netLocationListener, m_locMgrNET);
        }
    }


    public void askForLocationPermission() {
        ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.ACCESS_FINE_LOCATION},
                LOCATION_PERMISSION_CODE);
    }

    public boolean isLocationServiceTurnedOn() {
        LocationManager lm = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
        boolean gps_enabled = false;
        try {
            gps_enabled = lm.isProviderEnabled(LocationManager.GPS_PROVIDER);
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return gps_enabled;
    }

    @Override
    public void onBackPressed() {
        if (Build.VERSION.SDK_INT > Build.VERSION_CODES.LOLLIPOP) {
            finishAndRemoveTask();
        } else {
            finish();
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode,
                                           String permissions[], int[] grantResults) {
        switch (requestCode) {
            case LOCATION_PERMISSION_CODE: {
                // If request is cancelled, the result arrays are empty.
                if (grantResults.length > 0
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    useCurrentLocationInSearchBar();
                } else {
                    CommonFunctions.showMessage(
                            this,
                            CommonFunctions.getStringName("searchBarLocationPermissionNotGrantedTitle", this),
                            CommonFunctions.getStringName("searchBarLocationPermissionNotGranted", this)
                    );
                }
                return;
            }
        }
    }

    private void setLocationParamsInSearchBar(Location location, LocationListener locationListener, LocationManager locationManager) {
        locationManager.removeUpdates(locationListener);
        locationListener = null;
        locationManager = null;
        FragmentManager l_fm = getSupportFragmentManager();
        SearchBar l_searchBar = (SearchBar) l_fm.findFragmentById(R.id.mainHolder);
        l_searchBar.setLocationParams(location.getLatitude(), location.getLongitude());
    }

    private void useCurrentLocationInSearchBar() {
        FragmentManager fm = getSupportFragmentManager();
        SearchBar fragment = (SearchBar) fm.findFragmentById(R.id.mainHolder);
        fragment.useMyLocation();
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_camera) {
            // Handle the camera action
        } else if (id == R.id.nav_gallery) {

        } else if (id == R.id.nav_slideshow) {

        } else if (id == R.id.nav_manage) {

        } else if (id == R.id.nav_share) {

        } else if (id == R.id.nav_send) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    public ArrayList<RestaurantDetails> getM_resRestaurantDetails() {
        return m_resRestaurantDetails;
    }

    public void setM_resRestaurantDetails(ArrayList<RestaurantDetails> resRestaurantDetails) {
        this.m_resRestaurantDetails = resRestaurantDetails;
//        this.m_resRestaurantDetails = new ArrayList<RestaurantDetails>();
//        for (RestaurantDetails res:
//                resRestaurantDetails) {
//            if (res.getThumbnailBase64String() != null)
//                m_resRestaurantDetails.add(res);
//        }
    }
}
