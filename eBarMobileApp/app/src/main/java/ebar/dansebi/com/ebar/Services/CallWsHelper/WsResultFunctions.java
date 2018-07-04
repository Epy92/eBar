package ebar.dansebi.com.ebar.Services.CallWsHelper;

import android.content.Context;
import android.support.design.widget.CoordinatorLayout;

import ebar.dansebi.com.ebar.Features.CommonFunctions;

/**
 * Created by sebas on 3/14/2018.
 */

public class WsResultFunctions {

    public static void showConnectionErrorMessage(int callResultCode, Context context, CoordinatorLayout coordinatorLayout){
        switch (callResultCode) {
            case -4:
                CommonFunctions.showBottomMessage(CommonFunctions.getStringName("unexpected_error_encountered", context), coordinatorLayout);
                break;
            case -3:
                CommonFunctions.showBottomMessage(CommonFunctions.getStringName("no_internet_connection", context), coordinatorLayout);
                break;
            case -2:
                CommonFunctions.showBottomMessage(CommonFunctions.getStringName("weak_internet_connection", context), coordinatorLayout);
                break;
            case -1:
                CommonFunctions.showBottomMessage(CommonFunctions.getStringName("cannot_connect_to_server", context), coordinatorLayout);
                break;
        }
    }
}
