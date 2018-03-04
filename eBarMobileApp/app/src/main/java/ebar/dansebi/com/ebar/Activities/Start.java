package ebar.dansebi.com.ebar.Activities;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.Gravity;
import android.view.WindowManager;

import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Features.SQLiteDB;
import ebar.dansebi.com.ebar.Features.SessionParameters;
import ebar.dansebi.com.ebar.Objects.CONNECTION_SETTINGS;
import ebar.dansebi.com.ebar.R;

public class Start extends AppCompatActivity {

    public static final int SPLASH_SCREEN_TIMEOUT = 2000;
    private static final int PERMISSIONS_REQUEST_ACCESS_FINE_LOCATION = 405;
    public static Context context;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start);
        context = this;
        checkAndroidOSPermissions();

        SQLiteDB myDB = new SQLiteDB(context);
        SessionParameters.setMyDB(myDB);

        SessionParameters.setTimerForSendingDevTokenToServerIsRunning(false);
        SessionParameters.setSendDeviceTokenToServerIsRunning(false);

        verifyConnectionSettings();

        setSplashScreen();
    }

    private void setSplashScreen() {
        try {
            new Handler().postDelayed(new Runnable() {
                @Override
                public void run() {
                    final ProgressDialog progressDialog = new ProgressDialog(context, R.style.ProgressDialogTransparentBackground);
                    progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
                    progressDialog.setCancelable(false);
                    progressDialog.setCanceledOnTouchOutside(false);
                    progressDialog.setMessage("Loading...");
                    WindowManager.LayoutParams windowParams = progressDialog.getWindow().getAttributes();
                    windowParams.gravity = Gravity.BOTTOM;
                    windowParams.y = 600;
                    progressDialog.getWindow().setAttributes(windowParams);
                    progressDialog.show();
                    new Handler().postDelayed(new Runnable() {
                        @Override
                        public void run() {
                            if (progressDialog.isShowing())
                                progressDialog.dismiss();
                            if (SessionParameters.isGetAndroidLocationGranted()) {
                                Intent intent = new Intent(Start.this, Home.class);
                                finish();
                                startActivity(intent);
                            }
                        }
                    }, SPLASH_SCREEN_TIMEOUT);
                }
            }, 1000);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    private void verifyConnectionSettings() {
        CONNECTION_SETTINGS connection_settings = MySharedPreferences.getConnectionSettings(this);
        if (connection_settings.getWsAddress() == null || connection_settings.getWsPassword() == null || connection_settings.getWsUsername() == null)
            MySharedPreferences.setConnectionSettings(this);
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
                    Intent intent = new Intent(Start.this, Home.class);
                    finish();
                    startActivity(intent);
                }
            }
        }
    }

}
