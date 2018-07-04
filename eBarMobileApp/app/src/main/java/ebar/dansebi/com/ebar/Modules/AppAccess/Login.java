package ebar.dansebi.com.ebar.Modules.AppAccess;

import android.content.Context;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.TextInputEditText;
import android.support.v4.app.Fragment;
import android.support.v7.app.AlertDialog;
import android.support.v7.widget.CardView;
import android.text.TextUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import ebar.dansebi.com.ebar.Activities.AppAccessActivity;
import ebar.dansebi.com.ebar.Features.CommonFunctions;
import ebar.dansebi.com.ebar.Objects.User;
import ebar.dansebi.com.ebar.R;
import ebar.dansebi.com.ebar.Services.UserServices.RecoverPasswordServices.GetPasswordResetCodeTask;
import ebar.dansebi.com.ebar.Services.UserServices.LoginServices.LoginTask;


public class Login extends Fragment {

    private View m_view;
    private CoordinatorLayout m_coordLayout;
    private Context m_context;
    private Fragment m_fragment;

    public Login() {
        // Required empty public constructor
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        m_view = inflater.inflate(R.layout.fragment_login, container, false);
        m_context = m_view.getContext();
        m_fragment = this;
        m_coordLayout = (CoordinatorLayout) m_view.findViewById(R.id.login_coordLayout);
        TextView l_forgotPass = (TextView) m_view.findViewById(R.id.login_tvForgotPassword);
        final TextInputEditText l_etEmail = (TextInputEditText) m_view.findViewById(R.id.login_email_et);
        final TextInputEditText l_etPass = (TextInputEditText) m_view.findViewById(R.id.login_password_et);
        CardView btnLogin = (CardView) m_view.findViewById(R.id.login_btnLogin);
        CardView btnCreateAccount = (CardView) m_view.findViewById(R.id.login_btnCreateAccount);

        l_forgotPass.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                getPassResetCode();
            }
        });

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (checkFieldsCompleted(l_etEmail, l_etPass) == 1) {
                    User l_userToLogIn = new User(l_etEmail.getText().toString(), l_etPass.getText().toString());
                    LoginTask l_loginTask = new LoginTask(m_context, m_coordLayout, l_userToLogIn);
                    l_loginTask.execute();
                } else {
                    CommonFunctions.showBottomMessage(CommonFunctions.getStringName("complete_all_fields", m_context), m_coordLayout);
                    CommonFunctions.shortVibrate(m_view);
                }

            }
        });

        btnCreateAccount.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                AppAccessActivity.switchFragment(2);
            }
        });

        //set underline to apper hyperlink
        l_forgotPass.setPaintFlags(l_forgotPass.getPaintFlags() | Paint.UNDERLINE_TEXT_FLAG);

        return m_view;
    }

    private int checkFieldsCompleted(EditText email, EditText pass) {
        int l_code = -1;
        if (!TextUtils.isEmpty(email.getText().toString()) && !TextUtils.isEmpty(pass.getText().toString()))
            l_code = 1;
        else if (TextUtils.isEmpty(email.getText().toString()) && TextUtils.isEmpty(pass.getText().toString()))
            l_code = -1;
        else if (TextUtils.isEmpty(email.getText().toString()))
            l_code = -2;
        else
            l_code = -3;

        return l_code;
    }

    private void getPassResetCode() {
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(m_context);
        LayoutInflater inflater = m_fragment.getActivity().getLayoutInflater();
        final View dialogView = inflater.inflate(R.layout.custom_dialog_get_pass_reset_code, null);
        dialogBuilder.setView(dialogView);
        final AlertDialog alertDialog = dialogBuilder.create();

        final TextInputEditText l_inputUsername = (TextInputEditText) dialogView.findViewById(R.id.recoverpassword_username_et);
        CardView btnGetResetCode = (CardView) dialogView.findViewById(R.id.recoverpassword_btnSendCode);
        CardView btnResetPassword = (CardView) dialogView.findViewById(R.id.recoverpassword_btnResetPassword);
        final CoordinatorLayout l_cdCoordLayout = (CoordinatorLayout) dialogView.findViewById(R.id.forgotpassword_coordLayout);

        btnGetResetCode.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!l_inputUsername.getText().toString().trim().isEmpty()) {
                    GetPasswordResetCodeTask l_getPassResetCode = new GetPasswordResetCodeTask(m_context, getActivity(), l_cdCoordLayout, alertDialog, new User(l_inputUsername.getText().toString()));
                    l_getPassResetCode.execute();
                } else {
                    CommonFunctions.showBottomMessage(CommonFunctions.getStringName("complete_username_or_email", m_context), l_cdCoordLayout);
                }
            }
        });

        btnResetPassword.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                alertDialog.dismiss();
                AppAccessActivity.switchFragment(3);
            }
        });

        alertDialog.show();
        alertDialog.getWindow().setDimAmount(0.9f);
        alertDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));

    }

    public static void showRecoverPassSuccessMsgDialog(Context context, String message){
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(context);
        LayoutInflater inflater = ((AppAccessActivity) context).getLayoutInflater();
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

        tvTitle.setText(CommonFunctions.getStringName("success", context));
        tvMessage.setText(message);
        alertDialog.show();
        alertDialog.getWindow().setDimAmount(0.8f);
        alertDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
    }
}
