package ebar.dansebi.com.ebar.Services.UserServices.LoginServices;

import android.content.Context;
import android.os.AsyncTask;
import android.support.design.widget.CoordinatorLayout;

import com.google.gson.reflect.TypeToken;

import org.json.JSONException;
import org.json.JSONObject;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashMap;

import ebar.dansebi.com.ebar.Activities.AppAccessActivity;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceRequestTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResponse;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultCodes;
import ebar.dansebi.com.ebar.Features.CommonFunctions;
import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Objects.User;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;
import ebar.dansebi.com.ebar.Features.EbarProgressDialog;
import ebar.dansebi.com.ebar.Services.CallWsHelper.GenericRunService;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultFunctions;

/**
 * Created by sebas on 3/1/2018.
 */

public class LoginTask extends AsyncTask<Void, Void, Void> {
    private Context m_context;
    private CoordinatorLayout m_coordLayout;
    private User m_currentUser;
    private EbarProgressDialog m_progressDialog;
    private WsCallResult m_wsCallResult;

    public LoginTask(Context context, CoordinatorLayout coordinatorLayout, User user) {
        m_context = context;
        m_coordLayout = coordinatorLayout;
        m_currentUser = user;
        m_wsCallResult = new WsCallResult();
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
        m_progressDialog = new EbarProgressDialog(m_context);
        m_progressDialog.show();
    }


    @Override
    protected Void doInBackground(Void... voids) {
        try {
            JSONObject l_loginJSON = new JSONObject();
            l_loginJSON.put("Username", m_currentUser.getUsername());
            l_loginJSON.put("UserPassword", m_currentUser.getUserPassword());

            HashMap<String, String> l_params = new HashMap<>();
            l_params.put("jsonData", l_loginJSON.toString());

            Type listType = new TypeToken<WsResponse>(){}.getType();
            GenericRunService l_grs = new GenericRunService();
            m_wsCallResult = l_grs.CallWebservice(WebserviceTypes.UsersService, WebserviceRequestTypes.POST,
                    "UserLogin", l_params, m_context, listType);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        if (m_progressDialog.isShowing()) {
            m_progressDialog.dismiss();
        }

        if (m_wsCallResult.getWsResponse() != null) {
            if (((WsResponse) m_wsCallResult.getWsResponse()).isResultFlag()) {
                MySharedPreferences.setLoggedInUser(m_currentUser, m_context);
                AppAccessActivity.openMainActivity();
            } else {
                switch (WsResultCodes.valueOf(((WsResponse)m_wsCallResult.getWsResponse()).getResultCode())) {
                    case MissingUser:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("inexistent_username_or_email", m_context), m_coordLayout);
                        break;
                    case OperationFailed:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("server_error", m_context), m_coordLayout);
                        break;
                }
            }
        } else {
            WsResultFunctions.showConnectionErrorMessage(m_wsCallResult.getCallResult(), m_context, m_coordLayout);
        }
    }
}
