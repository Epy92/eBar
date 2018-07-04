package ebar.dansebi.com.ebar.Activities;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Build;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.TextView;

import ebar.dansebi.com.ebar.Modules.AppAccess.Login;
import ebar.dansebi.com.ebar.Modules.AppAccess.ResetPassword;
import ebar.dansebi.com.ebar.Modules.AppAccess.Register;
import ebar.dansebi.com.ebar.R;

public class AppAccessActivity extends AppCompatActivity {
    FrameLayout fragmentHolder;
    private static FragmentManager fragmentManager;
    private static Activity m_activity;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_appaccess);
        fragmentHolder = (FrameLayout) findViewById(R.id.flFragmentHolder);
        fragmentManager = this.getSupportFragmentManager();
        m_activity = this;
        switchFragment(1);//enter login form
    }

    public static void switchFragment(int code) {
        switch (code) {
            case 1:
                fragmentManager.beginTransaction().replace(R.id.flFragmentHolder, new Login()).commit();
                break;
            case 2:
                fragmentManager.beginTransaction().replace(R.id.flFragmentHolder, new Register()).commit();
                break;
            case 3:
                fragmentManager.beginTransaction().replace(R.id.flFragmentHolder, new ResetPassword()).commit();
                break;
        }
    }

    @Override
    public void onBackPressed() {
        Fragment l_currFragmentLoaded = fragmentManager.findFragmentById(R.id.flFragmentHolder);

        if (l_currFragmentLoaded instanceof Login) {
            askCloseApplication();
        } else if (l_currFragmentLoaded instanceof Register) {
            switchFragment(1);
        } else if (l_currFragmentLoaded instanceof ResetPassword) {
            switchFragment(1);
        }
    }

    private void askCloseApplication(){
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(this);
        LayoutInflater inflater = this.getLayoutInflater();
        final View dialogView = inflater.inflate(R.layout.custom_dialog_yn, null);
        dialogBuilder.setView(dialogView);
        final AlertDialog alertDialog = dialogBuilder.create();

        TextView tvTitle = (TextView) dialogView.findViewById(R.id.tvTitleDialogTemplateYNMode);
        TextView tvMessage = (TextView) dialogView.findViewById(R.id.tvMessageDialogTemplateYNMode);
        Button btnNo = (Button) dialogView.findViewById(R.id.btnNoDialogTemplateYNMode);
        Button btnYes = (Button) dialogView.findViewById(R.id.btnYesDialogTemplateYNMode);


        btnNo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                alertDialog.dismiss();
            }
        });

        btnYes.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                alertDialog.dismiss();
                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
                    finishAndRemoveTask();
                } else {
                    finish();
                }
            }
        });
        tvTitle.setText("EXIT APPLICATION");
        tvMessage.setText("Are you sure you want to close the application?");
        alertDialog.show();
        alertDialog.getWindow().setDimAmount(0.8f);
        alertDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
    }

    public static void openMainActivity(){
        Intent l_intent = new Intent(m_activity, MainActivity.class);
        m_activity.finish();
        m_activity.startActivity(l_intent);
    }



}
