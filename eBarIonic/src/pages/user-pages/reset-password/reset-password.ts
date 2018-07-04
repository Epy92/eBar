import { Component } from '@angular/core';
import { NavController, AlertController, LoadingController, Loading, IonicPage, Events } from 'ionic-angular';
import { UserServiceProvider } from '../../../providers/user-service/user-service';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';

@IonicPage()
@Component({
  selector: 'page-reset-password',
  templateUrl: 'reset-password.html',
  providers: [UserServiceProvider]
})
export class ResetPasswordPage {
  loading: Loading;
  sendRestCodeForm : FormGroup;
  email : null;

  constructor(private nav: NavController, private userService: UserServiceProvider, private alertCtrl: AlertController, private loadingCtrl: LoadingController, 
    public formBuilder: FormBuilder,) {
      this.sendRestCodeForm = this.formBuilder.group({
        email: ['', Validators.compose([Validators.required, Validators.pattern("(?:[a-z0-9!#$%&\'*+=?^_\'{|}~-]+(?:\\.[a-z0-9!#$%&\'*+=?^_\'{|}~-]+)*|)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9])])")])]
      }); 
  }

  sendResetCode()
  {
    this.showDialog('Va rugam asteptati');
    this.email = this.sendRestCodeForm.controls.email.value.toString();
    var sendRestCodeResponse = this.userService.sendResetCode(this.email);

      if (sendRestCodeResponse[0] == true) {
        if(sendRestCodeResponse[1] == "ResetCodeGenerated")
        {
          this.presentConfirm('Codul de resetare a fost generat si trimis pe emailul dumneavoastra.');
        }
        else{
          this.presentConfirm('Aveti deja un cod generat care inca mai este activ. ');
        }
      } else {
        this.showError("Codul de resetare nu a putut fi trimis. Va rugam verificati daca email-ul introdus este corect si incercati din nou.");
      }
  }

  showDialog(text) {
    this.loading = this.loadingCtrl.create({
      content: text,
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  presentConfirm(text) {
    let alert = this.alertCtrl.create({
      title: 'Resetare parola',
      message: text + ' Doriti sa continuati catre restarea parolei?',
      buttons: [
        {
          text: 'Nu acum',
          role: 'cancel',
          handler: () => {
              let alert = this.alertCtrl.create({
                title: 'Info',
                subTitle: 'Nu uitati: Codul de resetare este valabil 60 min!',
                buttons: ['OK']
              });
              alert.present();
          }
        },
        {
          text: 'Da',
          handler: () => {
            this.nav.push('RestPasswordPage');
          }
        }
      ]
    });
    alert.present();
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

  goToResetPassword(){
    this.nav.push('RestPasswordPage');
  }
}
