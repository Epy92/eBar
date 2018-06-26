import { Injectable } from '@angular/core';
import { Platform } from 'ionic-angular';
import { Http } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class EBarServiceProvider {

  constructor(public http: Http, 
    public platform: Platform) {

}

  getRestaurantsByCity(city: string ): Observable<any> {
    return new Observable(observer => {
      this.platform.ready().then(
          location => this.apiRequestGetByCity(city).subscribe(
            res => {
              observer.next(res.results);
              observer.complete();
            }
          )
        )
    })
  }

  apiRequestGetByCity(city: string): Observable<any> {
    let url = 'http://localhost/eBarWS/api/Restaurant/GetRestaurantsByCity/' + city;
    //let query = `?lat=${location['coords']['latitude']}&long=${location['coords']['longitude']}&q=${searchTerm}`;

    return this.http.get(url).map(res => res.json());
  }

  getAllRestaurants(searchTerm: string = 'Restaurant'): Observable<any> {
    return new Observable(observer => {
      this.platform.ready().then(
        location => this.apiRequestGetAllRest().subscribe(
          res => {
            observer.next(res);
            observer.complete();
          }
        )
      )
    })
}

  apiRequestGetAllRest(): Observable<any> {
    let url = 'http://localhost/eBarWS/api/Restaurant/GetRestaurantsForPr';
    
    return this.http.get(url).map(res => res.json());
  }

  getArray(size): Array<any> {
    return new Array(size);
  }
}
