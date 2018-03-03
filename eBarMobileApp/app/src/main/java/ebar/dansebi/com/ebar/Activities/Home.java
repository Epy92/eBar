package ebar.dansebi.com.ebar.Activities;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
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

import ebar.dansebi.com.ebar.Fragments.Login;
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

        switchFragment(1);//enter login form
    }

    private void switchFragment(int code){
        switch (code){
            case 1:
                fragmentManager.beginTransaction().replace(R.id.flFragmentHolder, new Login()).commit();
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
    }



}
