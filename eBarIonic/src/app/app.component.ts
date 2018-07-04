import { Component, ViewChild, ElementRef } from '@angular/core';
import { Platform, MenuController, Nav, Events } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { UserServiceProvider } from '../providers/user-service/user-service';

import { IndexPage } from '../pages/index/index';
import { UserHomepagePage } from '../pages/user-homepage/user-homepage';
import { RestaurantDetailsPage } from '../pages/restaurant-pages/restaurant-details/restaurant-details';
import { RestaurantEventsPage } from '../pages/restaurant-pages/restaurant-events/restaurant-events';
import {ProductCategoriesPage} from '../pages/restaurant-pages/product-categories/product-categories';
import {RestaurantProductsPage} from '../pages/restaurant-pages/restaurant-products/restaurant-products';
import {RestaurantPhotosPage} from '../pages/restaurant-pages/restaurant-photos/restaurant-photos';
import {RestaurantEmployeesPage} from '../pages/restaurant-pages/restaurant-employees/restaurant-employees';

export interface PageInterface {
  title: string;
  component: any;  
  icon: string;
}

@Component({
  templateUrl: 'app.html',
  providers: [UserServiceProvider],
})

export class MyApp {
  @ViewChild(Nav) nav: Nav;
  @ViewChild('authButtons') authButtons: ElementRef;
  @ViewChild('helloUser') helloUser: ElementRef;

  rootPage: any = IndexPage;
  userData = null;
  pages: PageInterface[];
  isUserLogged: boolean = false;

  constructor(public platform: Platform, public menu: MenuController, public statusBar: StatusBar, public splashScreen: SplashScreen,
    private userService: UserServiceProvider, public events: Events) {
    this.initializeApp();
    
    this.events.subscribe('user:logged', (time) => {
      this.authButtons.nativeElement.style.display = 'none';
      this.userService.getUser().then(user => {
        this.helloUser.nativeElement.innerText = user.username;
        this.isUserLogged = true;
        this.menu.enable(true);
      });
    });

    this.checkUserSession();

    // set our app's pages
    this.pages = [
      { title:'Dashboard', component:UserHomepagePage, icon:'assets/icon/icons8-dashboard-filled-50.png' },
      { title: 'Detalii restaurant', component: RestaurantDetailsPage, icon: "assets/icon/icons8-restaurant-building-filled-50.png" },
      { title: 'Evenimente', component: RestaurantEventsPage, icon: "assets/icon/icons8-reserve-filled-50.png" },
      { title: 'Categorii produse', component: ProductCategoriesPage, icon: "assets/icon/icons8-restaurant-menu-filled-50.png" },
      { title: 'Produse', component: RestaurantProductsPage, icon: "assets/icon/icons8-meal-filled-50.png" },
      { title: 'Fotografii', component: RestaurantPhotosPage, icon: "assets/icon/icons8-camera-filled-50.png" },
      { title: 'Personal restaurant', component: RestaurantEmployeesPage, icon: "assets/icon/icons8-people-filled-50.png" },
    ];
  }

  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.styleDefault();
      this.splashScreen.hide();
      this.menu.enable(false);
      this.menu.close();
    });
  }

  checkUserSession(){
    this.userService.getUser().then(user=>{
      if(user != null)
      {
        this.helloUser.nativeElement.innerText = user.username;
        this.isUserLogged = true;
        this.menu.enable(true);
        this.authButtons.nativeElement.style.display = 'none';
      }
    });
  }

  getSidebarStatus() {
    return this.isUserLogged;
  }

  openPage(page) {
    //this.rootPage = page.component;
    this.nav.push(page.component);
  }

  goToRegister() {
    this.nav.push('RegisterPage');
  }

  goToLogin() {
    this.nav.push('LoginPage');
  }

  goToHome() {
    this.nav.popToRoot();
  }

  logout() {
    this.userService.logout();
    this.authButtons.nativeElement.style.display = 'block';
    this.isUserLogged = false;
    this.menu.enable(false);
    this.nav.popToRoot();
  }

  goToUserProfile(){

  }
}
