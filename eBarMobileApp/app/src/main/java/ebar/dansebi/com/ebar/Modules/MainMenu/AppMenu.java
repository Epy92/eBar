package ebar.dansebi.com.ebar.Modules.MainMenu;

import android.content.Context;
import android.os.Bundle;
import android.support.design.widget.CoordinatorLayout;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import ebar.dansebi.com.ebar.Activities.MainActivity;
import ebar.dansebi.com.ebar.Modules.SearchBar.Fragment.SearchBar;
import ebar.dansebi.com.ebar.R;


public class AppMenu extends Fragment {

    private View m_view;
    private CoordinatorLayout m_coordLayout;
    private Context m_context;
    private Fragment m_fragment;

    public AppMenu() {
        // Required empty public constructor
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        m_view = inflater.inflate(R.layout.fragment_appmenu, container, false);
        m_context = m_view.getContext();
        m_fragment = this;
        TextView l_welcomeMsg = (TextView) m_view.findViewById(R.id.appMenu_tvWelcome);
        l_welcomeMsg.setText("Hello Sebi Dan");

        CardView l_cvSearchBar = (CardView) m_view.findViewById(R.id.framentAppMenuCvSearchBar);


        l_cvSearchBar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ((MainActivity) getActivity()).switchFragment(2);
                getActivity().getSupportFragmentManager().beginTransaction().replace(R.id.mainHolder, new SearchBar()).commit();
            }
        });

        return m_view;
    }

}
