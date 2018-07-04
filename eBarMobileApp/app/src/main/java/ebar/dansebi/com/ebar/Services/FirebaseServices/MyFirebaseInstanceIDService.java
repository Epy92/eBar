package ebar.dansebi.com.ebar.Services.FirebaseServices;

import com.google.firebase.iid.FirebaseInstanceId;
import com.google.firebase.iid.FirebaseInstanceIdService;

import ebar.dansebi.com.ebar.Features.MySharedPreferences;

/**
 * Created by sebas on 7/27/2017.
 */

public class MyFirebaseInstanceIDService extends FirebaseInstanceIdService {

    @Override
    public void onTokenRefresh() {
        String deviceToken = FirebaseInstanceId.getInstance().getToken();
        MySharedPreferences.setSmartphoneDetails(this, deviceToken);
    }
}
