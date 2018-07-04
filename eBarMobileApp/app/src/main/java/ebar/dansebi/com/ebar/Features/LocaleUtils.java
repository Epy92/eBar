package ebar.dansebi.com.ebar.Features;

import android.app.Activity;
import android.content.res.Configuration;

import java.util.Locale;

/**
 * Created by sebas on 3/10/2018.
 */

public class LocaleUtils {

    public static void changeAppLocale(String langCode, Activity activity) {
        SessionParameters.setLanguageCode(langCode.toUpperCase().equals("RO") ? 1 : 2);
        Locale locale = new Locale(langCode);
        Locale.setDefault(locale);
        Configuration config = new Configuration();
        config.locale = locale;
        activity.getBaseContext().getResources().updateConfiguration(config, activity.getBaseContext().getResources().getDisplayMetrics());
    }
}