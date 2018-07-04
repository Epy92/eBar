import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { RequestOptions } from '@angular/http';
 import { Http, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { Storage } from "@ionic/storage";
// import { NativeStorage } from '@ionic-native/native-storage';

export class User {
  email: string;
  name: string;
  username: string;
  sessionKey: string;

  constructor(email: string, sessionKey: string, name: string, username: string) {
    this.email = email;
    this.sessionKey = sessionKey;
    this.name = name;
    this.username = username;
  }
}

@Injectable()
export class UserServiceProvider {
  User: User;
  isLoggedIn: boolean;

  constructor(public http: Http, private storage: Storage) {
    this.storage.get('user').then((user) => {
      this.User = user;
      this.isLoggedIn = true;
    });
  }

  login(credentials) {
    if (credentials.email === null || credentials.password === null) {
      return Observable.throw("Date de logare inexistente");
    } else {
      return Observable.create(observer => {
        //var loginResponse = null;
        let loginUrl = "http://localhost/eBarWS/api/UserSession/UserLogin"

        let headers = new Headers(
          {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin" : "*"
          });
        let options = new RequestOptions({ headers: headers });

        let body = { Email: credentials.email, UserPassword: credentials.password };

        this.http.post(loginUrl, JSON.stringify(body), options)
          .subscribe(
            res => {
              var user = null;
              // loginResponse = res.json();
              var jsonData = JSON.parse(res.json());
              let access = (jsonData.ResultFlag == true && jsonData.ResultCode == "LoginSuccess");
              if (access) {
                user = new User(jsonData.UserDetails.Email, jsonData.SessionKey, jsonData.UserDetails.Name, jsonData.UserDetails.Username)
                this.setUser(user).then(() => {
                  observer.next(access);
                  observer.complete();
                });
              }
            },
            err => {
              console.log("Error occured:" + err);
              observer.next(false);
              observer.complete();
            }
          );
      });
    }
  }

  async getUser() {
    await this.storage.get('user').then((user) => {
        this.User = user;
        this.isLoggedIn = true;
    });
    return this.User;
  }

  async setUser(user: User) {
    await this.storage.set('user', user).then(() => {
        this.isLoggedIn = true;
        this.User = user;
    })
  }

  deleteUserData() {
    this.storage.remove('user').then(() => {
      this.isLoggedIn = false;
      this.User = null;
    });
  }

  public register(credentials) {
    if (credentials["email"].value == null || credentials["password"].value == null) {
      return Observable.throw("Please insert required data");
    } else {
      return Observable.create(observer => {
        let registerUrl = "http://localhost/eBarWS/api/UserSession/Register";
        var registerResponse = null;

        let headers = new Headers(
          {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin" : "*"
          });
        let options = new RequestOptions({ headers: headers });

        let body = { Email: credentials["email"].value, UserPassword: credentials["password"].value, Name: credentials["name"].value, Username: credentials["username"].value, UserTypeId: 2 };

        this.http.post(registerUrl, JSON.stringify(body), options)
          .subscribe(
            res => {
              registerResponse = res.json();
              let access = (JSON.parse(registerResponse).ResultFlag == true && JSON.parse(registerResponse).ResultCode == "RegisterSuccess");
              //var userDetails = JSON.parse(JSON.parse(loginResponse).UserDetails);
              //this.currentUser = new User(userDetails.Email, JSON.parse(loginResponse).SessionKey, userDetails.Name, userDetails.Username)
              observer.next(access);
              observer.complete();
            },
            err => {
              console.log("Error occured:" + err);
              observer.next(false);
              observer.complete();
            }
          );
      });
    }
  }

  public sendResetCode(email) {
    if (email == null || email == '') {
      return Observable.throw("No email");
    } else {
      let generateResetCodeUrl = "http://localhost/eBarWS/api/UserSession/GenerateResetCode/" + email;
      var generateResetCodeResponse = null;
      var returnObj = [];
      this.http.get(generateResetCodeUrl).map(res => {
        generateResetCodeResponse = res.json();
        returnObj.push((JSON.parse(generateResetCodeResponse).ResultFlag == true));
        returnObj.push(JSON.parse(generateResetCodeResponse).ResultCode);
      },
        err => {
          console.log("Error occured on reset code");
        });
    }
    return returnObj;
  }

  public logout() {
    this.deleteUserData();
  }
}

