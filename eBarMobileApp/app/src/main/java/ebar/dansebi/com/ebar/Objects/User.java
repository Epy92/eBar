package ebar.dansebi.com.ebar.Objects;

import ebar.dansebi.com.ebar.Features.SessionParameters;

/**
 * Created by sebas on 3/1/2018.
 */

public class User {
    private String Username, Email, UserPassword, Name;
    private int UserTypeId, UserPreferredLanguageID;

    public User(String Username, String Email, String UserPassword, String Name) {
        this.Username = Username;
        this.Email = Email;
        this.UserPassword = UserPassword;
        this.Name = Name;
        this.UserTypeId = 1;
        this.UserPreferredLanguageID = SessionParameters.getLanguageCode();
    }

    public User(String Username, String UserPassword) {
        this.Username = Username;
        this.UserPassword = UserPassword;
        this.UserTypeId = 1;
    }

    public User(String Username) {
        this.Username = Username;
        this.UserTypeId = 1;
    }

    public User() {
        this.UserTypeId = 1;
    }

    public String getUsername() {
        return Username;
    }

    public void setUsername(String username) {
        Username = username;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getUserPassword() {
        return UserPassword;
    }

    public void setUserPassword(String userPassword) {
        UserPassword = userPassword;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public int getUserTypeId() {
        return UserTypeId;
    }

    public void setUserTypeId(int userTypeID) {
        UserTypeId = userTypeID;
    }

    public int getUserPreferredLanguageID() {
        return UserPreferredLanguageID;
    }

    public void setUserPreferredLanguageID() {
        UserPreferredLanguageID = SessionParameters.getLanguageCode();
    }
}
