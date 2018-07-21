//import { HttpClient } from '@angular/common/http';
import { Http, Headers } from '@angular/http';
import { Injectable } from '@angular/core';
import { Platform } from 'ionic-angular';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { RequestOptions } from '@angular/http';
import { UserServiceProvider } from '../../providers/user-service/user-service';

@Injectable()
export class RestaurantEventsProvider {
sessionKey:String;

  constructor(public http: Http, public platform: Platform, private userService: UserServiceProvider) {
    console.log('Hello RestaurantEventsProvider Provider');
    this.userService.getUser().then(user => {
      if (user != null) {
        this.sessionKey = user.sessionKey;
      }
    });
  }

  getAllEvents(userId: number): Observable<any> {
    return new Observable(observer => {
      this.platform.ready().then(
        location => this.getEventsByAdministrator(userId).subscribe(
          res => {
            observer.next(res);
            observer.complete();
          }
        )
      )
    })
  }

  getEventsByAdministrator(userId: number): Observable<any> {
    //let url = 'http://localhost/eBarWS/api/Restaurant/GetRestarantEvents/' + administratorEmail;
    let url = 'http://localhost/eBarWS/api/Restaurant/GetRestarantEvents/?restaurantId=1';
    
    let headers = new Headers(
      {
        "SessionID": this.sessionKey
      });
    let options = new RequestOptions({ headers: headers });
    return this.http.get(url).map(res => res.json());
  }
}
