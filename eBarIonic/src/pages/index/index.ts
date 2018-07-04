import { Component } from '@angular/core'
import { NavController, NavParams, Events, Platform } from 'ionic-angular';
import { EBarServiceProvider } from '../../providers/e-bar-service/e-bar-service';
import { UserServiceProvider } from '../../providers/user-service/user-service';
import { LoginPage } from '../user-pages/login/login';

@Component({
  selector: 'page-index',
  templateUrl: 'index.html',
  providers: [EBarServiceProvider, UserServiceProvider]
})
export class IndexPage {
  restaurants: Array<any>;

  constructor(public navCtrl: NavController,
    public navParams: NavParams,
    public restaurantService: EBarServiceProvider, private userService: UserServiceProvider,public events: Events, public platform: Platform) {
  }

  ionViewWillEnter() {
    this.userService.getUser().then(user=>{
      if(user != null)
      {
        this.navCtrl.push("UserHomepagePage");        
      }
      else{
        if(this.platform.is('core') || this.platform.is('mobileweb')) {
          this.getRestaurants();
        } else {
          this.navCtrl.setRoot(LoginPage);
        }
      }
    });   
  }

  getRestaurants(refresher?: any) {
    this.restaurantService.getAllRestaurants()
      .subscribe(res => {
        this.restaurants = JSON.parse(res);
        
        var restPR = this.restaurants;
        for(var i=0; i < restPR.length; i++)
        {
          var restaurantImageBase64 = restPR[i].ThumbnailBase64String;
          if(restaurantImageBase64 != null)
          {
            restPR[i].ThumbnailBase64String = "data:image/jpeg;base64," + restaurantImageBase64;
          }
        }
        this.restaurants = null;
        this.restaurants = restPR;

        if (refresher) {
          refresher.complete();
        }
      });
  }
  

  showDetails(details: Object) {
    //this.navCtrl.push(Details, details);
  }
}
