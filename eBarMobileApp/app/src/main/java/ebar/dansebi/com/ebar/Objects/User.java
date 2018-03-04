package ebar.dansebi.com.ebar.Objects;

/**
 * Created by sebas on 3/1/2018.
 */

public class User {
    private String Username, Email, UserPassword, Name;

    public User(String Username, String Email, String UserPassword, String Name) {
        this.Username = Username;
        this.Email = Email;
        this.UserPassword = UserPassword;
        this.Name = Name;
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
}
