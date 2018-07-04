import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { HTTP } from '@ionic-native/http';

import { IndexPage } from '../pages/index/index';
import { RestaurantDetailsPageModule } from '../pages/restaurant-pages/restaurant-details/restaurant-details.module';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { EBarServiceProvider } from '../providers/e-bar-service/e-bar-service';

import { Component } from '@angular/core';
import { UserServiceProvider } from '../providers/user-service/user-service';
import { IonicStorageModule } from '@ionic/storage';
import { RestaurantDetailsPage } from '../pages/restaurant-pages/restaurant-details/restaurant-details';
import { UserHomepagePage } from '../pages/user-homepage/user-homepage';
import { UserHomepagePageModule } from '../pages/user-homepage/user-homepage.module';
import { RestaurantProductsPage } from '../pages/restaurant-pages/restaurant-products/restaurant-products';
import { ProductCategoriesPage } from '../pages/restaurant-pages/product-categories/product-categories';
import { RestaurantPhotosPage } from '../pages/restaurant-pages/restaurant-photos/restaurant-photos';
import { RestaurantEmployeesPage } from '../pages/restaurant-pages/restaurant-employees/restaurant-employees';
import { RestaurantEventsPage } from '../pages/restaurant-pages/restaurant-events/restaurant-events';
import { RestaurantEventsPageModule } from '../pages/restaurant-pages/restaurant-events/restaurant-events.module';
import { ProductCategoriesPageModule } from '../pages/restaurant-pages/product-categories/product-categories.module';
import { RestaurantProductsPageModule } from '../pages/restaurant-pages/restaurant-products/restaurant-products.module';
import { RestaurantPhotosPageModule } from '../pages/restaurant-pages/restaurant-photos/restaurant-photos.module';
import { RestaurantEmployeesPageModule } from '../pages/restaurant-pages/restaurant-employees/restaurant-employees.module';
import { LoginPage } from '../pages/user-pages/login/login';
import { RegisterPage } from '../pages/user-pages/register/register';
import { LoginPageModule } from '../pages/user-pages/login/login.module';
import { RegisterPageModule } from '../pages/user-pages/register/register.module';

@Component({
  templateUrl: 'app.html'
})

@NgModule({
  declarations: [
    MyApp,
    IndexPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp),
    HttpModule,
    HttpClientModule,
    IonicStorageModule.forRoot({
      name: '__mydb',
    }),
    RestaurantDetailsPageModule,
    UserHomepagePageModule,
    RestaurantEventsPageModule,
    ProductCategoriesPageModule,
    RestaurantProductsPageModule,
    RestaurantPhotosPageModule,
    RestaurantEmployeesPageModule,
    LoginPageModule,
    RegisterPageModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    IndexPage,
    LoginPage,
    RegisterPage,
    RestaurantDetailsPage,
    UserHomepagePage,
    RestaurantEventsPage,
    ProductCategoriesPage,
    RestaurantProductsPage,
    RestaurantPhotosPage,
    RestaurantEmployeesPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    EBarServiceProvider,
    UserServiceProvider,
    HTTP
  ]
})
export class AppModule {}
