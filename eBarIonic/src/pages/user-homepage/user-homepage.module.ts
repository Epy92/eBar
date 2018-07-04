import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { UserHomepagePage } from './user-homepage';

@NgModule({
  declarations: [
    UserHomepagePage
  ],
  imports: [
    IonicPageModule.forChild(UserHomepagePage),
  ],
})
export class UserHomepagePageModule {}
