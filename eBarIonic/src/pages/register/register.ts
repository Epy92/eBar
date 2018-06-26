import { Component } from '@angular/core';
import { NavController, AlertController,NavParams, LoadingController, Loading, IonicPage } from 'ionic-angular';
import { UserServiceProvider } from '../../providers/user-service/user-service';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';

@IonicPage()
@Component({
  selector: 'page-register',
  templateUrl: 'register.html',
  providers: [UserServiceProvider]
})
export class RegisterPage {
  loading: Loading;
  userForm : FormGroup;

  constructor(public navCtrl: NavController, public navParams: NavParams, public formBuilder: FormBuilder,  private userService: UserServiceProvider,
    private loadingCtrl: LoadingController, private alertCtrl: AlertController) {
    this.userForm = this.formBuilder.group({
      Name: ['', Validators.required],
      Username: ['', Validators.required,],
      UserTypeId: '',
      Prenume: ['', Validators.required],
      Email: ['', Validators.compose([Validators.required, Validators.pattern("(?:[a-z0-9!#$%&\'*+=?^_\'{|}~-]+(?:\\.[a-z0-9!#$%&\'*+=?^_\'{|}~-]+)*|)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9])])")])],
      UserPassword: ['', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(12), Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,12}$')])],
      UserConfirmPassword: ['', Validators.required],
    }, {validator: this.matchingPasswords('UserPassword', 'UserConfirmPassword')}); 
  }

  register(){
    this.showLoading()
    this.userService.register(this.userForm).subscribe(allowed => {
      if (allowed) {        
        this.navCtrl.push('LoginPage');
      } else {
        this.showError("Access restrictionat");
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

  matchingPasswords(passwordKey: string, confirmPasswordKey: string) {
    // TODO maybe use this https://github.com/yuyang041060120/ng2-validation#notequalto-1
    return (group: FormGroup): {[key: string]: any} => {
      let password = group.controls[passwordKey];
      let confirmPassword = group.controls[confirmPasswordKey];

      if (password.value !== confirmPassword.value) {
        return {
          mismatchedPasswords: true
        };
      }
    }
  }

  checkUsername(usernameKey : string)
  {
    return {
      usernameExists: false
    };
  }
}
