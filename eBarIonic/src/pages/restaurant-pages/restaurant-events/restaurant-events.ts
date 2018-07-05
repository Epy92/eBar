import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';


class RestaurantEventsModel{
  EventId:number;
  EventTitle:string;
  EventDescription:string;
  RestaurantThumbnail: string;
  HasThumbnail:boolean;
  StartDate:datetime;
  EndDate:datetime;
  StartDay:string;
  StartMonth:string;
  StartYear:string;
  StartTime:string;
  EndDay:string;
  EndMonth:string;
  EndYear:string;
  EndTime:string;
}

@Component({
  selector: 'page-restaurant-events',
  templateUrl: 'restaurant-events.html',
})

export class RestaurantEventsPage {
events: List<RestaurantEventsModel>;
  constructor(public navCtrl: NavController, public navParams: NavParams) {
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad RestaurantEventsPage');
  }
}
