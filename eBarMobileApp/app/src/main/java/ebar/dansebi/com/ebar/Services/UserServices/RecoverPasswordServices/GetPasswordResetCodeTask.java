package ebar.dansebi.com.ebar.Services.UserServices.RecoverPasswordServices;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.support.design.widget.CoordinatorLayout;
import android.support.v7.app.AlertDialog;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashMap;

import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceRequestTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResponse;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultCodes;
import ebar.dansebi.com.ebar.Features.CommonFunctions;
import ebar.dansebi.com.ebar.Modules.AppAccess.Login;
import ebar.dansebi.com.ebar.Objects.User;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;
import ebar.dansebi.com.ebar.Features.EbarProgressDialog;
import ebar.dansebi.com.ebar.Services.CallWsHelper.GenericRunService;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultFunctions;

/**
 * Created by sebas on 3/1/2018.
 */

public class GetPasswordResetCodeTask extends AsyncTask<Void, Void, Void> {
    private Context m_context;
    private CoordinatorLayout m_coordLayout;
    private Activity m_activity;
    private User m_currentUser;
    private EbarProgressDialog m_progressDialog;
    private AlertDialog m_parentDialog;
    private WsCallResult m_wsCallRes;

    public GetPasswordResetCodeTask(Context context, Activity activity, CoordinatorLayout coordinatorLayout, AlertDialog alertDialog, User user) {
        m_context = context;
        m_activity = activity;
        m_coordLayout = coordinatorLayout;
        m_parentDialog = alertDialog;
        m_currentUser = user;
        m_wsCallRes = new WsCallResult();
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
        m_progressDialog = new EbarProgressDialog(m_context);
        m_progressDialog.show();
    }


    @Override
    protected Void doInBackground(Void... voids) {
        try{
            HashMap<String, String> l_params = new HashMap<>();
            l_params.put("jsonData", new Gson().toJson(m_currentUser.getUsername()));

            GenericRunService l_grs = new GenericRunService();
            Type l_deserializedObjType = new TypeToken<WsResponse>(){}.getType();
            m_wsCallRes = l_grs.CallWebservice(WebserviceTypes.UsersService, WebserviceRequestTypes.POST,
                    "GenerateResetCode", l_params, m_context, l_deserializedObjType);
        }catch (Exception e){
            m_wsCallRes.setCallResult(-4);
            e.printStackTrace();
        }

        return null;
    }

    @Override
    protected void onPostExecute(Void res) {
        super.onPostExecute(res);
        if (m_progressDialog.isShowing()) {
            m_progressDialog.dismiss();
        }

        if (m_wsCallRes.getWsResponse() == null) {
            WsResultFunctions.showConnectionErrorMessage(m_wsCallRes.getCallResult(), m_context, m_coordLayout);
        } else {
            if (((WsResponse) m_wsCallRes.getWsResponse()).isResultFlag()) {
                m_parentDialog.dismiss();
                Login.showRecoverPassSuccessMsgDialog(m_context, CommonFunctions.getStringName("send_reset_code_on_email", m_context));
            } else {
                switch (WsResultCodes.valueOf(((WsResponse) m_wsCallRes.getWsResponse()).getResultCode())){
                    case MissingUser:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("inexistent_username_or_email", m_context), m_coordLayout);
                        break;
                    case ResetCodeAlreadyGenerated:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("reset_code_already_generated", m_context), m_coordLayout);
                        break;
                    case GenerateResetCodeFailed:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("generate_reset_code_failed", m_context), m_coordLayout);
                        break;
                    case OperationFailed:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("server_error", m_context), m_coordLayout);
                        break;
                }

            }
        }
    }


}
