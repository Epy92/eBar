import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';
import { UserServiceProvider } from '../../../providers/user-service/user-service';
import { RestaurantEventsProvider } from '../../../providers/restaurant-events/restaurant-events';

@IonicPage()
@Component({
  selector: 'page-save-event',
  templateUrl: 'save-event.html',
  providers: [RestaurantEventsProvider, UserServiceProvider]
})
export class SaveEventPage {
  event: any;
  actionMode:String;
  loggedUser:any;

  constructor(public navCtrl: NavController, public navParams: NavParams, private restEventsService: RestaurantEventsProvider, private userService:UserServiceProvider, private alertCtrl : AlertController) {
    this.event =  this.navParams.get('eventToRenew');
    if(this.event == null)
    {
      this.actionMode = "Adauga eveniment";
      this.event = new Object();
      this.event.EventStartDate = new Date();
      this.event.EventEndDate = new Date();
      this.event.EventTitle = "";
      this.event.EventDescription = "";
    }
    else{
      this.actionMode = "Editeaza eveniment";
    }
  }
  
  ionViewDidLoad() {
    console.log('ionViewDidLoad SaveEventPage');
    this.userService.getUser().then(user => {
      if (user == null) {
        this.showError("Acces neautorizat. Pentru a accesa pagina dorita te rugam sa te autentifici mai intai!");
      }
      else{
        this.loggedUser = user;
      }
    });
  }

  saveEvent(){

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
}
