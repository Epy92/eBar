import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading, IonicPage, Events, MenuController } from 'ionic-angular';
import { UserServiceProvider } from '../../../providers/user-service/user-service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
  providers: [UserServiceProvider]
})
export class LoginPage {
  loading: Loading;
  loginCredentials = { email: '', password: '' }
  loginFormData: FormGroup;

  constructor(private nav: NavController, private userService: UserServiceProvider, private alertCtrl: AlertController, private loadingCtrl: LoadingController, private menu: MenuController,
    public formBuilder: FormBuilder, public events: Events) {
    this.loginFormData = this.formBuilder.group({
      password: ['', Validators.required],
      email: ['', Validators.compose([Validators.required, Validators.pattern("(?:[a-z0-9!#$%&\'*+=?^_\'{|}~-]+(?:\\.[a-z0-9!#$%&\'*+=?^_\'{|}~-]+)*|)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9])])")])]
    });
  }

  public createAccount() {
    this.nav.push('RegisterPage');
  }

  public login() {
    this.showLoading()
    this.loginCredentials.email = this.loginFormData.controls.email.value.toString();
    this.loginCredentials.password = this.loginFormData.controls.password.value.toString();
    this.userService.login(this.loginCredentials).subscribe(allowed => {
      if (allowed) {
        this.events.publish('user:logged', Date.now());
        this.nav.push('UserHomepagePage');
      } else {
        this.showError("Nu s-a putut face logare! Va rugam verificati datele introduse si incercati din nou.");
      }
    },
    error => {
      this.showError(error);
    });
  }

  showLoading() {
    this.loading = this.loadingCtrl.create({
      content: 'Va rugam asteptati',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  showError(text) {
    this.loading.dismiss();

    let alert = this.alertCtrl.create({
      title: 'Eroare',
      subTitle: text,
      buttons: ['OK']
    });
    alert.present();
  }

  goToSendResetCode() {
    this.nav.push('SendResetCodePage');
  }

  onPageDidLeave() {
    // enable the left menu when leaving the login page
    this.menu.enable(true);
  }
}
