package ebar.dansebi.com.ebar.Features;

import android.app.Dialog;
import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.AnimationDrawable;
import android.graphics.drawable.ColorDrawable;
import android.widget.ImageView;

import ebar.dansebi.com.ebar.R;

/**
 * Created by sebas on 3/6/2018.
 */

public class EbarProgressDialog extends Dialog {

    public EbarProgressDialog(Context context) {
        super(context);
        setContentView(R.layout.custom_loading_spinner);
        getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        setCancelable(false);
        setCanceledOnTouchOutside(false);
        ImageView m_spinner = (ImageView) findViewById(R.id.custom_spinner);
        m_spinner.setBackgroundResource(R.drawable.spinner_anim);
        AnimationDrawable frameAnimation = (AnimationDrawable) m_spinner.getBackground();
        frameAnimation.start();
    }
}
