package ebar.dansebi.com.ebar.Services.UserServices.RegisterServices;

import android.content.Context;
import android.os.AsyncTask;
import android.support.design.widget.CoordinatorLayout;

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
import ebar.dansebi.com.ebar.Modules.AppAccess.Register;
import ebar.dansebi.com.ebar.Objects.User;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;
import ebar.dansebi.com.ebar.Features.EbarProgressDialog;
import ebar.dansebi.com.ebar.Services.CallWsHelper.GenericRunService;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultFunctions;

/**
 * Created by sebas on 3/1/2018.
 */

public class RegisterTask extends AsyncTask<Void, Void, Void> {
    private Context m_context;
    private CoordinatorLayout m_coordLayout;
    private EbarProgressDialog m_progressDialog;
    private User m_userAccount;
    private WsCallResult m_wsCallRes;

    public RegisterTask(Context context, CoordinatorLayout coordinatorLayout, User user) {
        m_context = context;
        m_coordLayout = coordinatorLayout;
        m_userAccount = user;
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

        try {
            HashMap<String, String> l_params = new HashMap<>();
            l_params.put("jsonData", new Gson().toJson(m_userAccount));

            GenericRunService l_grs = new GenericRunService();
            Type l_deserializedObjType = new TypeToken<WsResponse>(){}.getType();
            m_wsCallRes = l_grs.CallWebservice(WebserviceTypes.UsersService, WebserviceRequestTypes.POST,
                    "Register", l_params, m_context, l_deserializedObjType);
        } catch (Exception e) {
            m_wsCallRes.setCallResult(-4);
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
        if (m_wsCallRes.getWsResponse() == null) {
            WsResultFunctions.showConnectionErrorMessage(m_wsCallRes.getCallResult(), m_context, m_coordLayout);
        } else {
            if (((WsResponse) m_wsCallRes.getWsResponse()).isResultFlag()) {
                Register.showRegisterSuccessMsgDialog(m_context, ((WsResponse) m_wsCallRes.getWsResponse()).getResultMessage());
            } else {
                switch (WsResultCodes.valueOf(((WsResponse) m_wsCallRes.getWsResponse()).getResultCode())) {
                    case UserInvalid:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("invalid_username_or_email", m_context), m_coordLayout);
                        break;
                    case DuplicateUser:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("duplicate_username_or_email", m_context), m_coordLayout);
                        break;
                    case OperationFailed:
                        CommonFunctions.showBottomMessage(CommonFunctions.getStringName("server_error", m_context), m_coordLayout);
                        break;
                }
            }
        }
    }

}
