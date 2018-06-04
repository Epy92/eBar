import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { IndexPage } from '../pages/index/index';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { EBarServiceProvider } from '../providers/e-bar-service/e-bar-service';

import { Component } from '@angular/core';
import { UserServiceProvider } from '../providers/user-service/user-service';

@Component({
  templateUrl: 'app.html'
})

@NgModule({
  declarations: [
    MyApp,
    IndexPage,
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp),
    HttpModule,
    HttpClientModule,
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    IndexPage,
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    EBarServiceProvider,
    UserServiceProvider
  ]
})
export class AppModule {}
