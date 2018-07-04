package ebar.dansebi.com.ebar.Modules.SearchBar.Classes;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.util.Base64;
import android.widget.ImageView;

import java.lang.ref.WeakReference;

/**
 * Created by sebas on 6/28/2018.
 */

public class DecodeBas64ToBitmapTask extends AsyncTask<RestaurantDetails, Void, Bitmap> {
    private final WeakReference<ImageView> imageViewReference;

    public DecodeBas64ToBitmapTask(ImageView imgView) {
        imageViewReference = new WeakReference<>(imgView);
    }

    @Override
    protected Bitmap doInBackground(RestaurantDetails... params) {
        try {
            if (params[0].getThumbnailBase64String() == null){
                return null;
            }
            byte[] decodedString = Base64.decode(params[0].getThumbnailBase64String(), Base64.DEFAULT);
            params[0].setThumbnailBase64String(null);
            return BitmapFactory.decodeByteArray(decodedString, 0, decodedString.length);
      //      return Bitmap.createScaledBitmap(bmp, 800, 600, false);
        } catch (Exception e) {
            params[0].setThumbnailBase64String(null);
            return null;
        }
    }

    protected void onPostExecute(Bitmap s) {
        super.onPostExecute(s);
        if (s != null) {
            if (imageViewReference != null) {
                final ImageView imageView = imageViewReference.get();
                if (imageView != null) {
                    imageView.setImageBitmap(s);
                }
            }
        }
    }
}
