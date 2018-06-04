import { Component, ViewChild, ElementRef } from '@angular/core';
import { Platform, MenuController, Nav, App, Events } from 'ionic-angular';
import { IndexPage } from '../pages/index/index';
import { LoginPage } from '../pages/login/login';
import { RegisterPage } from '../pages/register/register';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { UserServiceProvider } from '../providers/user-service/user-service';
import { UserSession } from '../models/UserSession';
import { SendResetCodePage } from '../pages/send-reset-code/send-reset-code';
import { ResetPasswordPage } from '../pages/reset-password/reset-password';

@Component({
  templateUrl: 'app.html',
  providers: [UserServiceProvider]
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;
  @ViewChild('authButtons') authButtons:ElementRef;
  @ViewChild('userData') userDataHtml:ElementRef;
  @ViewChild('helloUser') helloUser :ElementRef;

  rootPage = IndexPage;
  userData = null;
  pages: Array<{title: string, component: any}>;

  constructor(public platform: Platform, public menu: MenuController, public statusBar: StatusBar, public splashScreen: SplashScreen, 
    private userService: UserServiceProvider, public events: Events) {
    this.initializeApp();
    events.subscribe('user:logged', (time) => {
      this.authButtons.nativeElement.style.display = 'none';
      this.userDataHtml.nativeElement.style.display = 'block';
      this.helloUser.nativeElement.innerText = UserSession.getUser().username;
    });
    
    // set our app's pages
    this.pages = [
      { title: 'Index', component: IndexPage },
      { title: 'Login', component: LoginPage },
      { title: 'Register', component: RegisterPage },
      { title: 'SendResetCode', component: SendResetCodePage },
      { title: 'ResetPAssword', component: ResetPasswordPage}
    ];
  }
  
  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  openPage(page) {
    // close the menu when clicking a link from the menu
    this.menu.close();
    // navigate to the new page if it is not the current page
    this.nav.setRoot(page.component);
  }
     
    goToRegister(){
      this.nav.push('RegisterPage');
    }

    goToLogin(){
      this.nav.push('LoginPage');
    }

    goToHome(){
      this.nav.popToRoot();
    }

    logout()
    {
      this.userService.logout();
      this.authButtons.nativeElement.style.display = 'block';
      this.userDataHtml.nativeElement.style.display = 'none';
      this.nav.popToRoot();
    }
}
