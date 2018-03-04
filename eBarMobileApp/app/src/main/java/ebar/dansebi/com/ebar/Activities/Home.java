package ebar.dansebi.com.ebar.Activities;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;

import ebar.dansebi.com.ebar.Fragments.Login;
import ebar.dansebi.com.ebar.Fragments.Register;
import ebar.dansebi.com.ebar.R;

public class Home extends AppCompatActivity {
    FrameLayout fragmentHolder;
    FragmentManager fragmentManager;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        fragmentHolder = (FrameLayout) findViewById(R.id.flFragmentHolder);
        fragmentManager = this.getSupportFragmentManager();

        switchFragment(1, fragmentManager);//enter login form
    }

    public static void switchFragment(int code, FragmentManager fragmentManager){
        switch (code){
            case 1:
                fragmentManager.beginTransaction().replace(R.id.flFragmentHolder, new Login()).commit();
                break;
            case 2:
                fragmentManager.beginTransaction().replace(R.id.flFragmentHolder, new Register()).commit();
                break;
        }
    }
    public void GoToNewsAndEvents(View v){
    }
    public void GoToBar(View v){
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        Fragment l_currFragmentLoaded = fragmentManager.findFragmentById(R.id.flFragmentHolder);
//        if (l_currFragmentLoaded instanceof Login)
//            ;
//            // do something with f
//            ((CustomFragmentClass) f).doSomething();
    }



}
