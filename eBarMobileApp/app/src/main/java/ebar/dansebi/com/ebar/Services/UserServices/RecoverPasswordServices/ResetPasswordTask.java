package ebar.dansebi.com.ebar.Services.UserServices.RecoverPasswordServices;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.AsyncTask;
import android.support.design.widget.CoordinatorLayout;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.google.gson.reflect.TypeToken;

import org.json.JSONObject;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Dictionary;
import java.util.HashMap;
import java.util.Map;

import ebar.dansebi.com.ebar.Activities.AppAccessActivity;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceRequestTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WebserviceTypes;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResponse;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultCodes;
import ebar.dansebi.com.ebar.Features.CommonFunctions;
import ebar.dansebi.com.ebar.Objects.User;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsCallResult;
import ebar.dansebi.com.ebar.Features.EbarProgressDialog;
import ebar.dansebi.com.ebar.R;
import ebar.dansebi.com.ebar.Services.CallWsHelper.GenericRunService;
import ebar.dansebi.com.ebar.Services.CallWsHelper.WsResultFunctions;

/**
 * Created by sebas on 3/1/2018.
 */

public class ResetPasswordTask extends AsyncTask<Void, Void, Void> {
    private Context m_context;
    private CoordinatorLayout m_coordLayout;
    private Activity m_activity;
    private User m_currentUser;
    private String m_resetCode;
    private EbarProgressDialog m_progressDialog;
    private WsCallResult m_wsCallRes;

    public ResetPasswordTask(Context context, Activity activity, CoordinatorLayout coordinatorLayout, User user, String resetCode) {
        m_context = context;
        m_activity = activity;
        m_coordLayout = coordinatorLayout;
        m_currentUser = user;
        m_resetCode = resetCode;
        m_wsCallRes = null;
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
            JSONObject l_resetPassJSON = new JSONObject();
            l_resetPassJSON.put("username", m_currentUser.getUsername().toString());
            l_resetPassJSON.put("newPassword", m_currentUser.getUserPassword().toString());
            l_resetPassJSON.put("resetCode", m_resetCode);

            HashMap<String, String> l_params = new HashMap<>();
            l_params.put("jsonData", l_resetPassJSON.toString());

            Type l_deserializedObjType = new TypeToken<WsResponse>(){}.getType();
            GenericRunService l_grs = new GenericRunService();
            m_wsCallRes = l_grs.CallWebservice(WebserviceTypes.UsersService, WebserviceRequestTypes.POST,
                    "ResetUserPassword", l_params, m_context, l_deserializedObjType);
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
                showSuccessMessage();
            } else {
                switch (WsResultCodes.valueOf(((WsResponse) m_wsCallRes.getWsResponse()).getResultCode())) {
                    case MissingUser:
                    case UserInvalid:
                    case OperationFailed:
                }
            }
        }
    }

    private void showSuccessMessage() {
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(m_context);
        LayoutInflater inflater = m_activity.getLayoutInflater();
        final View dialogView = inflater.inflate(R.layout.custom_dialog_ok, null);
        dialogBuilder.setView(dialogView);
        final AlertDialog alertDialog = dialogBuilder.create();

        TextView tvTitle = (TextView) dialogView.findViewById(R.id.tvTitleDialogTemplateOkMode);
        TextView tvMessage = (TextView) dialogView.findViewById(R.id.tvMessageDialogTemplateOkMode);
        Button btnOk = (Button) dialogView.findViewById(R.id.btnYesDialogTemplateOkMode);


        btnOk.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                alertDialog.dismiss();
                AppAccessActivity.switchFragment(1);
            }
        });

        tvTitle.setText(CommonFunctions.getStringName("success", m_context));
        tvMessage.setText(CommonFunctions.getStringName("send_reset_code_on_email", m_context));
        alertDialog.show();
        alertDialog.getWindow().setDimAmount(0.8f);
        alertDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
    }
}
