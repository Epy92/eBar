import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';
import { UserServiceProvider } from '../../providers/user-service/user-service';

@IonicPage()
@Component({
  selector: 'page-restaurant-details',
  templateUrl: 'restaurant-details.html',
  providers: [UserServiceProvider]
})
export class RestaurantDetailsPage {

  userLogged:boolean = false;

  constructor(public navCtrl: NavController, public navParams: NavParams, private alertCtrl : AlertController, private userService:UserServiceProvider) {
    this.userLogged = this.userService.isLoggedIn;
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad RestaurantDetailsPage');
    this.userService.getUser().then(user => {
      if (user == null) {
        this.showError("Acces neautorizat. Pentru a accesa pagina dorita te rugam sa te autentifici mai intai!");
      }
    });
  }

  ionViewWillLeave() {
    console.log("Looks like I'm about to leave RestaurantDetailsPage");
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
