package ebar.dansebi.com.ebar.Features;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Vibrator;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import ebar.dansebi.com.ebar.Activities.AppAccessActivity;
import ebar.dansebi.com.ebar.R;

/**
 * Created by sebas on 3/4/2018.
 */

public class CommonFunctions {

    public static void showBottomMessage(String text, CoordinatorLayout coordinatorLayout) {
        Snackbar l_snackbar = Snackbar.make(coordinatorLayout, text, Snackbar.LENGTH_LONG);
        View l_snackbar_view = l_snackbar.getView();
        TextView l_textView = (TextView) l_snackbar_view.findViewById(android.support.design.R.id.snackbar_text);
        l_textView.setTextAlignment(View.TEXT_ALIGNMENT_VIEW_START);
        l_textView.setTextAlignment(View.TEXT_ALIGNMENT_CENTER);
        l_snackbar.show();
    }

    public static void shortVibrate(View v) {
        Vibrator l_vib = (Vibrator)  v.getContext().getSystemService(Context.VIBRATOR_SERVICE);
        l_vib.vibrate(200);
    }

    public static String getStringName(String strName, Context context){
        try{
            return context.getResources().getString(context.getResources().getIdentifier("string/" + strName, null, context.getPackageName()));
        }catch (Exception e){
            e.printStackTrace();

            return "";
        }
    }

    public static void showMessage(Context context, String title, String message) {
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(context);
        LayoutInflater inflater = ((Activity) context).getLayoutInflater();
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
            }
        });

        tvTitle.setText(title);
        tvMessage.setText(message);
        alertDialog.show();
        alertDialog.getWindow().setDimAmount(0.8f);
        alertDialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
    }
}
