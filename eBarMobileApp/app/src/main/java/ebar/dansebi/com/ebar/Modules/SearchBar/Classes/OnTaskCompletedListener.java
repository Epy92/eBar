package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

import android.content.Context;

import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;

/**
 * Created by sebas on 6/25/2018.
 */

public interface OnTaskCompletedListener {

    void callbackUI(WsCallResult wsCallResult);
}
