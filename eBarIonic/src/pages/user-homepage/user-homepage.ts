import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';
import { UserServiceProvider } from '../../providers/user-service/user-service';

@IonicPage()
@Component({
  selector: 'page-user-homepage',
  templateUrl: 'user-homepage.html',
  providers: [UserServiceProvider]
})
export class UserHomepagePage {
  userLogged = false;
  userHasRestaurant = true;

  constructor(public navCtrl: NavController, public navParams: NavParams, private userService: UserServiceProvider, private alertCtrl: AlertController) {
    this.userLogged = this.userService.isLoggedIn;
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad UserHomepagePage');
    this.userService.getUser().then(user => {
      if (user == null) {
        this.showError("Acces neautorizat. Pentru a accesa pagina dorita te rugam sa te autentifici mai intai!");
      }
    });
  }

  beginRestaurantConfig() {
    this.navCtrl.push("RestaurantDetailsPage", {pageMode: 'Edit'});
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
