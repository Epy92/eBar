package ebar.dansebi.com.ebar.Services.UserServices.LoginServices;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Handler;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.Snackbar;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.google.gson.reflect.TypeToken;

import org.json.JSONException;
import org.json.JSONObject;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashMap;

import ebar.dansebi.com.ebar.Activities.StartActivity;
import ebar.dansebi.com.ebar.Features.SessionParameters;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceRequestTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResponse;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultCodes;
import ebar.dansebi.com.ebar.Features.CommonFunctions;
import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Objects.User;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;
import ebar.dansebi.com.ebar.Services.CallWsHelper.GenericRunService;

/**
 * Created by sebas on 3/1/2018.
 */

public class AutomaticLoginTask extends AsyncTask<Void, Void, Void> {
    private Context m_context;
    private Activity m_callingActivity;
    private CoordinatorLayout m_coordLayout;
    private User m_currentUser;
    private boolean m_retryLogin;
    private WsCallResult m_wsCallResult;

    public AutomaticLoginTask(Context context, Activity activity, CoordinatorLayout coordinatorLayout, User user) {
        m_context = context;
        m_currentUser = user;
        m_callingActivity = activity;
        m_coordLayout = coordinatorLayout;
        m_retryLogin = false;
        m_wsCallResult = new WsCallResult();
    }

    @Override
    protected Void doInBackground(Void... voids) {
        try {
            JSONObject l_loginJSON = new JSONObject();
            l_loginJSON.put("Username", m_currentUser.getUsername());
            l_loginJSON.put("UserPassword", m_currentUser.getUserPassword());

            HashMap<String, String> l_params = new HashMap<>();
            l_params.put("jsonData", l_loginJSON.toString());

            GenericRunService l_grs = new GenericRunService();
            Type l_deserializedObjType = new TypeToken<WsResponse>(){}.getType();
            m_wsCallResult = l_grs.CallWebservice(WebserviceTypes.UsersService, WebserviceRequestTypes.POST,
                    "UserLogin", l_params, m_context,  l_deserializedObjType);
        } catch (JSONException e) {
            m_wsCallResult.setCallResult(-4);
            e.printStackTrace();
        }
        return null;
    }

    @Override
    protected void onPostExecute(Void res) {
        super.onPostExecute(res);
        StartActivity.setSpinnerVisibility(false);

        if (m_wsCallResult.getWsResponse() != null) {
            if (((WsResponse) m_wsCallResult.getWsResponse()).isResultFlag()) {
                MySharedPreferences.setLoggedInUser(m_currentUser, m_context);
                SessionParameters.setSessionKey(((WsResponse) m_wsCallResult.getWsResponse()).getSessionKey());
                StartActivity.openHomeActivity();
            } else {
                switch (WsResultCodes.valueOf(((WsResponse) m_wsCallResult.getWsResponse()).getResultCode())) {
                    case MissingUser:
                        StartActivity.openAppAccessActivity();
                        break;
                    case OperationFailed:
                        StartActivity.openAppAccessActivity();
                        break;
                }
            }
        } else {
            switch (m_wsCallResult.getCallResult()) {
                case -4:
                    showBottomMessage(CommonFunctions.getStringName("unexpected_error_encountered", m_context));
                    break;
                case -3:
                    showBottomMessage(CommonFunctions.getStringName("no_internet_connection", m_context));
                    break;
                case -2:
                    showBottomMessage(CommonFunctions.getStringName("weak_internet_connection", m_context));
                    break;
                case -1:
                    showBottomMessage(CommonFunctions.getStringName("cannot_connect_to_server", m_context));
                    break;
            }
        }
    }

    private void showBottomMessage(String message) {
        Snackbar l_snackbar = Snackbar.make(m_coordLayout, message, Snackbar.LENGTH_LONG);
        View l_snackbar_view = l_snackbar.getView();

        TextView l_textView = (TextView) l_snackbar_view.findViewById(android.support.design.R.id.snackbar_text);
        Button btn = (Button) l_snackbar_view.findViewById(android.support.design.R.id.snackbar_action);
        btn.setTextColor(Color.RED);
        l_textView.setTextAlignment(View.TEXT_ALIGNMENT_VIEW_START);
        l_snackbar.setDuration(Snackbar.LENGTH_INDEFINITE);
        l_snackbar.setAction("RETRY", new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                m_retryLogin = true;
                StartActivity.setSpinnerVisibility(true);
                new Handler().postDelayed(new Runnable() {
                    @Override
                    public void run() {
                        AutomaticLoginTask l_automaticLogin = new AutomaticLoginTask(m_context, m_callingActivity, m_coordLayout, m_currentUser);
                        l_automaticLogin.execute();
                    }
                }, 1500);
            }
        });
        l_snackbar.addCallback(new Snackbar.Callback() {

            @Override
            public void onDismissed(Snackbar snackbar, int event) {
                if (!m_retryLogin) {
                    StartActivity.quitApplication();
                }
            }
        });
        l_snackbar.show();
    }

}
