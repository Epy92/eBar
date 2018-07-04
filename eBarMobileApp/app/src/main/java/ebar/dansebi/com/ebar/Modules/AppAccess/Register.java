package ebar.dansebi.com.ebar.Modules.AppAccess;

import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.os.Vibrator;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.Snackbar;
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
import ebar.dansebi.com.ebar.Services.UserServices.RegisterServices.RegisterTask;


public class Register extends Fragment {

    private View m_view;
    private CoordinatorLayout m_coordLayout;
    private Context m_context;

    public Register() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        m_view = inflater.inflate(R.layout.fragment_register, container, false);
        m_context = m_view.getContext();
        m_coordLayout = (CoordinatorLayout) m_view.findViewById(R.id.register_coordLayout);
        final TextInputEditText l_etName = (TextInputEditText) m_view.findViewById(R.id.register_name_et);
        final TextInputEditText l_etUsername = (TextInputEditText) m_view.findViewById(R.id.register_username_et);
        final TextInputEditText l_etEmail = (TextInputEditText) m_view.findViewById(R.id.register_email_et);
        final TextInputEditText l_etPass = (TextInputEditText) m_view.findViewById(R.id.register_password_et);
        final TextInputEditText l_etConfirmPass = (TextInputEditText) m_view.findViewById(R.id.register_confirm_password_et);

        CardView btnRegister = (CardView) m_view.findViewById(R.id.register_btnRegisterAccount);
        btnRegister.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (checkFieldsCompleted(l_etName, l_etUsername, l_etEmail, l_etPass, l_etConfirmPass)) {
                    if (correctEmailCompleted(l_etEmail)) {
                        if (correctPasswordCompleted(l_etPass, l_etConfirmPass)) {
                            User l_userAccount = new User(l_etUsername.getText().toString(), l_etEmail.getText().toString(),
                                    l_etPass.getText().toString(), l_etName.getText().toString());
                            RegisterTask l_registerTask = new RegisterTask(m_context, m_coordLayout, l_userAccount);
                            l_registerTask.execute();
                        } else {
                            showMessage("Passwords do not match.", m_coordLayout);
                            ShortVibrate();
                        }
                    } else {
                        showMessage("Please enter a valid email address.", m_coordLayout);
                        ShortVibrate();
                    }
                } else {
                    showMessage("Please fill all the fields.", m_coordLayout);
                    ShortVibrate();
                }
            }
        });

        return m_view;
    }

    private boolean checkFieldsCompleted(EditText name, EditText username, EditText email, EditText pass, EditText confirmPass) {
        if (!TextUtils.isEmpty(name.getText().toString()) && !TextUtils.isEmpty(username.getText().toString())
                && !TextUtils.isEmpty(email.getText().toString()) && !TextUtils.isEmpty(pass.getText().toString())
                && !TextUtils.isEmpty(confirmPass.getText().toString())) {
            return true;
        } else {
            return false;
        }
    }

    private boolean correctEmailCompleted(EditText email) {
        return email.getText().toString().matches("(?:[a-z0-9!#$%&'*+\\=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+\\=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
    }

    private boolean correctPasswordCompleted(EditText pass, EditText confirmPass) {
        if (pass.getText().toString().equals(confirmPass.getText().toString())) {
            return true;
        } else {
            return false;
        }
    }

    public static void showMessage(String text, CoordinatorLayout coordinatorLayout) {
        Snackbar l_snackbar = Snackbar.make(coordinatorLayout, text, Snackbar.LENGTH_LONG);
        View l_snackbar_view = l_snackbar.getView();
        TextView l_textView = (TextView) l_snackbar_view.findViewById(android.support.design.R.id.snackbar_text);
        l_textView.setTextAlignment(View.TEXT_ALIGNMENT_VIEW_START);
        l_textView.setTextAlignment(View.TEXT_ALIGNMENT_CENTER);
        l_snackbar.show();
    }

    public void ShortVibrate() {
        Vibrator l_vib = (Vibrator) m_view.getContext().getSystemService(Context.VIBRATOR_SERVICE);
        l_vib.vibrate(200);
    }

    public static void showRegisterSuccessMsgDialog(Context context, String message){
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
