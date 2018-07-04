package ebar.dansebi.com.ebar.Activities;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.graphics.drawable.AnimationDrawable;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.NonNull;
import android.support.design.widget.CoordinatorLayout;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.ImageView;


import ebar.dansebi.com.ebar.Features.LocaleUtils;
import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Features.SessionParameters;
import ebar.dansebi.com.ebar.Services.CallWsHelper.ConnectionSettings;
import ebar.dansebi.com.ebar.Objects.User;
import ebar.dansebi.com.ebar.R;
import ebar.dansebi.com.ebar.Services.UserServices.LoginServices.AutomaticLoginTask;

public class StartActivity extends AppCompatActivity {

    public static Activity m_activity;
    public static ImageView m_spinner;
    private final int SPLASH_SCREEN_TIMEOUT = 3000;
    private final int PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION = 405;
    private Context m_context;
    private CoordinatorLayout m_coordLayout;

    @SuppressLint("ResourceType")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        m_context = getApplicationContext();
        m_activity = this;
        m_coordLayout = (CoordinatorLayout) findViewById(R.id.start_coord_layout);

        SessionParameters.setTimerForSendingDevTokenToServerIsRunning(false);
        SessionParameters.setSendDeviceTokenToServerIsRunning(false);
        verifyConnectionSettings();
        verifyLanguageSettings();

        setSplashScreen();
    }

    private void setSplashScreen() {
        try {
            new Handler().postDelayed(new Runnable() {
                @Override
                public void run() {
                    loadSpinner();
                    new Handler().postDelayed(new Runnable() {
                        @Override
                        public void run() {
                            User l_loggedInUser = MySharedPreferences.getLoggedInUser(m_context);
                            if (l_loggedInUser == null) {
                                Intent intent = new Intent(StartActivity.this, AppAccessActivity.class);
                                finish();
                                startActivity(intent);
                            } else {
                                AutomaticLoginTask l_automaticLogin = new AutomaticLoginTask(m_context, m_activity, m_coordLayout, l_loggedInUser);
                                l_automaticLogin.execute();
                            }
                        }
                    }, SPLASH_SCREEN_TIMEOUT);
                }
            }, 1000);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void quitApplication() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
            m_activity.finishAndRemoveTask();
        } else {
            m_activity.finish();
        }
    }

    @SuppressLint("ResourceType")
    private void loadSpinner() {
        m_spinner = (ImageView) findViewById(R.id.imageView);
        m_spinner.setBackgroundResource(R.drawable.spinner_anim);
        AnimationDrawable frameAnimation = (AnimationDrawable) m_spinner.getBackground();
        frameAnimation.start();
        // m_spinner.setWebViewClient(new MyWebViewClient());
//        m_spinner.getSettings().setJavaScriptEnabled(true);
//        m_spinner.setBackgroundColor(0x50000000);
//        m_spinner.setLayerType(WebView.LAYER_TYPE_SOFTWARE, null);
//        m_spinner.getSettings().setRenderPriority(WebSettings.RenderPriority.HIGH);
//        m_spinner.getSettings().setCacheMode(WebSettings.LOAD_NO_CACHE);
//        m_spinner.loadUrl("file:///android_asset/loading_html.html");
//        m_spinner.getSettings().setLoadWithOverviewMode(true);

    }

    public static void setSpinnerVisibility(boolean state) {
        m_spinner.setVisibility(state ? View.VISIBLE : View.INVISIBLE);
    }

    private void verifyConnectionSettings() {
        ConnectionSettings connectionSettings = MySharedPreferences.getConnectionSettings(m_context);
        if (connectionSettings.getWsAddress() == null || connectionSettings.getWsPassword() == null || connectionSettings.getWsUsername() == null)
            MySharedPreferences.setConnectionSettings(this);
    }

    private void verifyLanguageSettings() {
        if (!MySharedPreferences.isLanguageSet(m_context)) {
            MySharedPreferences.setDefaultLanguage(m_context);
        }

        LocaleUtils.changeAppLocale(MySharedPreferences.getDefaultLanguage(m_context), m_activity);
    }


    private void checkAndroidOSPermissions() {
        checkAndroidOSLocationPermission();
    }

    private void checkAndroidOSLocationPermission() {
        if (ContextCompat.checkSelfPermission(this.getApplicationContext(),
                android.Manifest.permission.ACCESS_FINE_LOCATION)
                != PackageManager.PERMISSION_GRANTED) {
            SessionParameters.setGetAndroidLocationGranted(false);

            ActivityCompat.requestPermissions(this,
                    new String[]{android.Manifest.permission.ACCESS_FINE_LOCATION},
                    PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION);
        } else {
            SessionParameters.setGetAndroidLocationGranted(true);
        }

    }

    @Override
    public void onRequestPermissionsResult(int requestCode,
                                           @NonNull String permissions[],
                                           @NonNull int[] grantResults) {
        switch (requestCode) {
            case PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION: {
                if (grantResults.length > 0
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    SessionParameters.setGetAndroidLocationGranted(true);
                    openAppAccessActivity();
                }
            }
        }
    }

    public static void openAppAccessActivity() {
        Intent intent = new Intent(m_activity, AppAccessActivity.class);
        m_activity.finish();
        m_activity.startActivity(intent);
    }

    public static void openHomeActivity() {
        Intent intent = new Intent(m_activity, MainActivity.class);
        m_activity.finish();
        m_activity.startActivity(intent);
    }

}
