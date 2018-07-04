package ebar.dansebi.com.ebar.Modules.AppAccess;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.TextInputEditText;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.text.TextUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import ebar.dansebi.com.ebar.Features.CommonFunctions;
import ebar.dansebi.com.ebar.Objects.User;
import ebar.dansebi.com.ebar.R;
import ebar.dansebi.com.ebar.Services.UserServices.RecoverPasswordServices.ResetPasswordTask;


public class ResetPassword extends Fragment {

    private View m_view;
    private CoordinatorLayout m_coordLayout;
    private Context m_context;
    private Fragment m_fragment;
    private Activity m_activity;

    public ResetPassword() {
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        m_view = inflater.inflate(R.layout.fragment_reset_password, container, false);
        m_context = m_view.getContext();
        m_fragment = this;
        m_activity = getActivity();
        m_coordLayout = (CoordinatorLayout) m_view.findViewById(R.id.resetpassword_coordLayout);
        final TextInputEditText l_etUsername = (TextInputEditText) m_view.findViewById(R.id.resetpassword_username_et);
        final TextInputEditText l_etResetCode = (TextInputEditText) m_view.findViewById(R.id.resetpassword_code_et);
        final TextInputEditText l_etPass = (TextInputEditText) m_view.findViewById(R.id.resetpassword_password_et);
        final TextInputEditText l_etConfirmpass = (TextInputEditText) m_view.findViewById(R.id.resetpassword_confirm_password_et);

        CardView btnResetPassword = (CardView) m_view.findViewById(R.id.resetpassword_btnResetpassword);

        btnResetPassword.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (checkAllFieldsCompleted(l_etUsername, l_etResetCode, l_etPass, l_etConfirmpass)) {
                    User m_currentUser = new User(l_etUsername.getText().toString(), l_etPass.getText().toString());
                    if (checkPasswordMatch(l_etPass, l_etConfirmpass)){
                        ResetPasswordTask l_resetPassTask = new ResetPasswordTask(m_context, m_activity, m_coordLayout, m_currentUser, l_etResetCode.getText().toString());
                        l_resetPassTask.execute();
                    }else{
                       CommonFunctions.showBottomMessage(CommonFunctions.getStringName("passwordsDoNotMatch", m_context), m_coordLayout);
                       CommonFunctions.shortVibrate(m_view);
                    }
                } else {
                    CommonFunctions.showBottomMessage(CommonFunctions.getStringName("complete_all_fields", m_context), m_coordLayout);
                    CommonFunctions.shortVibrate(m_view);
                }
            }
        });

        return m_view;
    }

    private boolean checkAllFieldsCompleted(TextInputEditText t1, TextInputEditText t2, TextInputEditText t3, TextInputEditText t4) {
        if (TextUtils.isEmpty(t1.getText().toString()) || TextUtils.isEmpty(t2.getText().toString())
                || TextUtils.isEmpty(t3.getText().toString()) || TextUtils.isEmpty(t4.getText().toString())) {
            return false;
        } else {
            return true;
        }
    }

    private boolean checkPasswordMatch(TextInputEditText t1, TextInputEditText t2){
        if (t1.getText().toString().equals(t2.getText().toString())){
            return true;
        }else{
            return false;
        }
    }

}
