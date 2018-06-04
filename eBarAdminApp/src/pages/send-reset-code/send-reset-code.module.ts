import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { SendResetCodePage } from './send-reset-code';

@NgModule({
  declarations: [
    SendResetCodePage,
  ],
  imports: [
    IonicPageModule.forChild(SendResetCodePage),
  ],
})
export class SendResetCodePageModule {}
