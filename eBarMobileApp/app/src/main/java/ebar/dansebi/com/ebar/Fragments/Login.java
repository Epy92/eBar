package ebar.dansebi.com.ebar.Fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.os.Vibrator;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.Snackbar;
import android.support.design.widget.TextInputEditText;
import android.support.design.widget.TextInputLayout;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.text.TextUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;

import ebar.dansebi.com.ebar.R;
import ebar.dansebi.com.ebar.Tasks.LoginTask;


public class Login extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View m_view;
    private CoordinatorLayout m_coordLayout;
    private Context m_context;
    private Fragment m_fragment;

    public Login() {
        // Required empty public constructor
    }

    public static Login newInstance(String param1, String param2) {
        Login fragment = new Login();
        Bundle args = new Bundle();
        fragment.setArguments(args);
        return fragment;
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
        final TextInputEditText l_etEmail = (TextInputEditText) m_view.findViewById(R.id.login_email_et);
        final TextInputEditText l_etPass = (TextInputEditText) m_view.findViewById(R.id.login_password_et);
        CardView btnLogin = (CardView) m_view.findViewById(R.id.login_btnLogin);
        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                switch (checkFieldsCompleted(l_etEmail, l_etPass)) {
                    case 1:
                        LoginTask l_loginTask = new LoginTask(m_context, l_etEmail.getText().toString(), l_etPass.getText().toString());
                        l_loginTask.execute();
                        break;
                    case -1:
                        //credentials not completed
                        showMessage("Please fill all the fields.");
                        ShortVibrate();
                        break;
                    case -2:
                        //email not completed
                        showMessage("Please fill the email field.");
                        ShortVibrate();
                        break;
                    case -3:
                        //pass not ompleted
                        showMessage("Please fill the password field.");
                        ShortVibrate();
                        break;
                }
            }
        });

        return m_view;
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
//        if (context instanceof OnFragmentInteractionListener) {
//            mListener = (OnFragmentInteractionListener) context;
//        } else {
//            throw new RuntimeException(context.toString()
//                    + " must implement OnFragmentInteractionListener");
//        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
//        mListener = null;
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
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

    public void showMessage(String text) {
        Snackbar l_snackbar = Snackbar.make(m_coordLayout, text, Snackbar.LENGTH_LONG);
        View l_snackbar_view = l_snackbar.getView();
        TextView l_textView = (TextView) l_snackbar_view.findViewById(android.support.design.R.id.snackbar_text);
        l_textView.setTextAlignment(View.TEXT_ALIGNMENT_VIEW_START);
        l_textView.setTextAlignment(View.TEXT_ALIGNMENT_CENTER);
        l_snackbar.show();
    }
    public void ShortVibrate() {
        Vibrator l_vib = (Vibrator)  m_view.getContext().getSystemService(Context.VIBRATOR_SERVICE);
        l_vib.vibrate(200);
    }

    private void clearFocus(TextInputEditText et1, TextInputEditText et2) {
        TextInputLayout til_pass = (TextInputLayout) m_view.findViewById(R.id.til_pass);
        TextInputLayout till_email = (TextInputLayout) m_view.findViewById(R.id.til_email);


        et1.clearFocus();
        till_email.clearFocus();
        et2.clearFocus();
        til_pass.clearFocus();
    }
}
