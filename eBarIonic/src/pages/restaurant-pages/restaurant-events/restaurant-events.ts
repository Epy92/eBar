import { Component } from '@angular/core';
import { NavController, NavParams,IonicPage, AlertController } from 'ionic-angular';
import { RestaurantEventsProvider } from '../../../providers/restaurant-events/restaurant-events';
import { UserServiceProvider } from '../../../providers/user-service/user-service';


class RestaurantEventsModel{
  RestaurantEventID:number;
  EventTitle:string;
  EventDescription:string;
  RestaurantThumbnail: string;
  HasThumbnail:boolean;
  EventStartDate:Date;
  EventEndDate:Date;
  StartDateText:String; 
  EndDateText:String;
}
@IonicPage()
@Component({
  selector: 'page-restaurant-events',
  templateUrl: 'restaurant-events.html',
  providers: [RestaurantEventsProvider, UserServiceProvider]
})

export class RestaurantEventsPage {
// events: Array<RestaurantEventsModel>;
events: Array<any>;
loggedUser:any;

  constructor(public navCtrl: NavController, public navParams: NavParams, private restEventsService: RestaurantEventsProvider, private userService:UserServiceProvider, private alertCtrl : AlertController) {
  }

  ionViewDidLoad() {
    this.userService.getUser().then(user => {
      if (user == null) {
        this.showError("Acces neautorizat. Pentru a accesa pagina dorita te rugam sa te autentifici mai intai!");
      }
      else{
        this.loggedUser = user;
        this.getEvents();
      }
    });
  }

  getEvents(refresher?: any) {
    this.restEventsService.getAllEvents(this.loggedUser.userId)
      .subscribe(res => {
        this.events = JSON.parse(res);
        if (refresher) {
          refresher.complete();
        }
      });
  }

  addEvent(){
    this.navCtrl.push("SaveEventPage", {eventToRenew: null});
  }

  renewEvent(event){
    this.navCtrl.push("SaveEventPage", {eventToRenew: event});
  }

  deleteEvent(event){
    this.presentDeleteEventConfirmationPopup(event.RestaurantEventID);
  }

  sendDeleteRequest(eventId)
  {
    
  }

  showError(text) {
    let alert = this.alertCtrl.create({
      title: 'Acces neautorizat',
      message: text,
      buttons: [
        {
          text: 'OK',
          handler: () => {
            this.navCtrl.push('LoginPage');
          }
        }
      ]
    });
    alert.present();
  }

  presentDeleteEventConfirmationPopup(eventId) {
    let alert = this.alertCtrl.create({
      title: 'Sterge eveniment',
      message: 'Doriti sa stergeti evenimentul?',
      buttons: [
        {
          text: 'Nu',
          role: 'cancel',
          handler: () => {}
        },
        {
          text: 'Da',
          handler: () => {
            this.sendDeleteRequest(eventId);
          }
        }
      ]
    });
    alert.present();
  }
}
