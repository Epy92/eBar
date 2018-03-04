package ebar.dansebi.com.ebar.Tasks;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.AsyncTask;
import android.view.Gravity;
import android.view.WindowManager;

import org.json.JSONArray;
import org.json.JSONObject;
import org.json.JSONTokener;
import org.kobjects.base64.Base64;
import org.ksoap2.HeaderProperty;
import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import java.util.ArrayList;

import ebar.dansebi.com.ebar.Activities.BarActivity;
import ebar.dansebi.com.ebar.Activities.Home;
import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Features.SessionParameters;
import ebar.dansebi.com.ebar.Objects.CONNECTION_SETTINGS;
import ebar.dansebi.com.ebar.Objects.MenuProduct;
import ebar.dansebi.com.ebar.R;

/**
 * Created by sebas on 8/2/2017.
 */

public class GetBarMenu extends AsyncTask<Void, Void, Void>{
    private ProgressDialog progressDialog;
    private String serverResponse;
    private Context m_context;
    public GetBarMenu(Context context) {
        this.m_context = context;
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
        progressDialog = new ProgressDialog(m_context, R.style.ProgressDialogTransparentBackground);
        progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
        progressDialog.setCancelable(false);
        progressDialog.setCanceledOnTouchOutside(false);
        progressDialog.setMessage("Loading...");
        WindowManager.LayoutParams windowParams = progressDialog.getWindow().getAttributes();
        windowParams.gravity = Gravity.BOTTOM;
        windowParams.y = 600;
        progressDialog.getWindow().setAttributes(windowParams);
        progressDialog.show();
    }

    @Override
    protected Void doInBackground(Void... params) {
//        CONNECTION_SETTINGS myConnSettings = MySharedPreferences.getConnectionSettings(m_context);
//       // myConnSettings.setMETHOD_NAME("GetProducts");
//
//     //   final SoapObject request = new SoapObject(myConnSettings.getNAMESPACE(), myConnSettings.getMETHOD_NAME());
//       // request.addProperty("BarName", SessionParameters.getCurrentBarname());
//
//        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER12);
//      //  envelope.setOutputSoapObject(request);
//        envelope.dotNet = true;
//
//        ArrayList<HeaderProperty> headers = new ArrayList<HeaderProperty>();
//      //  headers.add(new HeaderProperty("Authorization", "Basic " + Base64.encode((myConnSettings.getUSERNAME() + ":" + myConnSettings.getPASSWORD()).getBytes())));
//    //    HttpTransportSE androidHttpTransport = new HttpTransportSE(myConnSettings.getWEBSERVICE(), 5000);
//        try {
//       //     androidHttpTransport.call(myConnSettings.getSOAP_ACTION(), envelope, headers);
//            serverResponse = ((SoapObject) envelope.bodyIn).getPrimitivePropertySafelyAsString("return");
//        } catch (Exception e) {
//            e.printStackTrace();
//        }

        if (serverResponse != null)
            readBarMenuJSON();

        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        if (progressDialog.isShowing())
            progressDialog.dismiss();

        if (serverResponse != null){
            BarActivity.showBarMenu();
        }else{

        }
    }

    private void readBarMenuJSON(){
        try {
            JSONArray jsonProductsArray = (JSONArray) new JSONTokener(serverResponse).nextValue();
            for (int i = 0; i < jsonProductsArray.length(); i++){
                JSONObject jsonProduct = (jsonProductsArray.getJSONObject(i)).getJSONObject("PRODUCT");
                MenuProduct menuProduct = new MenuProduct();
                menuProduct.setProductName(jsonProduct.getString("PRODUCT_NAME"));
                menuProduct.setProductWeight(jsonProduct.getDouble("PRODUCT_WEIGHT"));
                menuProduct.setProductMadeOF(jsonProduct.getString("MADE_OF"));
                menuProduct.setProductType(jsonProduct.getString("PRODUCT_TYPE"));
                menuProduct.setProductCategory(jsonProduct.getString("PRODUCT_CATEGORY"));
                menuProduct.setProductPrice(jsonProduct.getDouble("PRODUCT_PRICE"));
                menuProduct.setProductUM(jsonProduct.getString("UM"));
                menuProduct.setProductCurrency(jsonProduct.getString("CURRENCY"));

                SessionParameters.addProductToBarMenu(menuProduct);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

    }
}
