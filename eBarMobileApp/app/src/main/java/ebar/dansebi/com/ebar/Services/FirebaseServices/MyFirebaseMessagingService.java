package ebar.dansebi.com.ebar.Services.FirebaseServices;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Intent;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.media.RingtoneManager;
import android.net.Uri;
import android.support.v4.app.NotificationCompat;

import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;

import org.json.JSONObject;
import org.json.JSONTokener;

import ebar.dansebi.com.ebar.Activities.NotificationsProcessing;
import ebar.dansebi.com.ebar.Objects.NOTIFICATION_JSON;
import ebar.dansebi.com.ebar.R;

public class MyFirebaseMessagingService extends FirebaseMessagingService {
    public MyFirebaseMessagingService() {
    }

    @Override
    public void onMessageReceived(RemoteMessage remoteMessage) {

        NOTIFICATION_JSON notification = getDataFromReceivedJSON(remoteMessage.getData().values().toArray()[0].toString());

        Uri uri = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);


        Intent activityIntent = new Intent(this, NotificationsProcessing.class);
        activityIntent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);

        PendingIntent contentIntent = PendingIntent.getActivity(this, 0, activityIntent, PendingIntent.FLAG_ONE_SHOT);


        NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
        builder.setSmallIcon(R.mipmap.ebar_icon)
                .setLargeIcon(BitmapFactory.decodeResource(getResources(), R.mipmap.ebar_icon_transparent))
                .setColor(Color.BLACK)
                .setContentTitle(notification.getTitle())
                .setContentIntent(contentIntent)
                .setContentText(notification.getMessage())
                .setDefaults(Notification.DEFAULT_ALL)
                .setPriority(Notification.PRIORITY_HIGH)
                .setSound(uri);

        NotificationManager manager = (NotificationManager) getSystemService(NOTIFICATION_SERVICE);
        manager.notify(0, builder.build());

    }

    private NOTIFICATION_JSON getDataFromReceivedJSON(String json) {
        NOTIFICATION_JSON notification = new NOTIFICATION_JSON();
        try {
            JSONObject jsonDataReceived = (JSONObject) ((JSONObject) new JSONTokener(json).nextValue()).get("Data");
            String title = jsonDataReceived.getString("Title");
            String message = jsonDataReceived.getString("Message");
            String eventType = jsonDataReceived.getString("EventType");

            notification.setMessage(message);
            notification.setEvent_type(eventType);
            notification.setTitle(title);
        } catch (Exception e) {
            e.getMessage();
        }

        return notification;
    }
}
