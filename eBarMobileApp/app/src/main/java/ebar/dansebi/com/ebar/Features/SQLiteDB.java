package ebar.dansebi.com.ebar.Features;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * Created by sebas on 7/26/2017.
 */

public class SQLiteDB extends SQLiteOpenHelper {

    private static int _dbVersion = 1;
    private static String _dbName = "eBar";
    private SQLiteDatabase sqLiteDatabase;
    private String QUERY_CREATE_DATABASE = "";

    public SQLiteDB(Context context) {
        super(context, _dbName, null, _dbVersion);
        sqLiteDatabase = getWritableDatabase();
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
       // sqLiteDatabase.execSQL(QUERY_CREATE_DATABASE);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }
}
