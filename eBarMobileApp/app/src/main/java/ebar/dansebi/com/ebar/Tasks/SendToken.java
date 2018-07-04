package ebar.dansebi.com.ebar.Tasks;

import android.content.Context;
import android.os.AsyncTask;

import org.ksoap2.serialization.SoapObject;

import java.util.Timer;

import ebar.dansebi.com.ebar.Features.MySharedPreferences;
import ebar.dansebi.com.ebar.Features.SessionParameters;

/**
 * Created by sebas on 7/28/2017.
 */

public class SendToken extends AsyncTask<Void, Void, Void> {

    private Context context;
    private SoapObject result;
    private String deviceToken, deviceUUID;
    private Boolean serverResponse;
    private Timer timer;

    public SendToken(Context context, String deviceToken, String deviceUUID, Timer timer) {
        this.context = context;
        this.result = null;
        this.serverResponse = false;
        this.deviceToken = deviceToken;
        this.deviceUUID = deviceUUID;
        this.timer = timer;
        SessionParameters.setSendDeviceTokenToServerIsRunning(true);
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
    }

    @Override
    protected Void doInBackground(Void... params) {

//        ConnectionSettings myConnSettings = MySharedPreferences.getConnectionSettings(context);
//        //myConnSettings.setMETHOD_NAME("RegisterOrUpdateUser");
//
//     //   final SoapObject request = new SoapObject(myConnSettings.getNAMESPACE(), myConnSettings.getMETHOD_NAME());
//        request.addProperty("UUID", deviceUUID);
//        request.addProperty("DeviceToken", deviceToken);
//
//        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER12);
//        envelope.setOutputSoapObject(request);
//        envelope.dotNet = true;
//
//        ArrayList<HeaderProperty> headers = new ArrayList<HeaderProperty>();
//        headers.add(new HeaderProperty("Authorization", "Basic " + Base64.encode((myConnSettings.getUSERNAME() + ":" + myConnSettings.getPASSWORD()).getBytes())));
//        HttpTransportSE androidHttpTransport = new HttpTransportSE(myConnSettings.getWEBSERVICE(), 5000);
//        try {
//            androidHttpTransport.call(myConnSettings.getSOAP_ACTION(), envelope, headers);
//            serverResponse = Boolean.valueOf(((SoapObject) envelope.bodyIn).getPrimitivePropertySafelyAsString("return"));
//        } catch (Exception e) {
//            e.printStackTrace();
//            serverResponse = false;
//        }
        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        boolean updated = MySharedPreferences.confirmDeviceTokenSentToServer(context, serverResponse);
        if (serverResponse && updated){
            timer.cancel();
            SessionParameters.setSendDeviceTokenToServerIsRunning(false);
            SessionParameters.setTimerForSendingDevTokenToServerIsRunning(false);
        }else{
            SessionParameters.setSendDeviceTokenToServerIsRunning(false);
        }


    }
}
