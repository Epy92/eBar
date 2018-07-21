import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { SaveEventPage } from './save-event';

@NgModule({
  declarations: [
    SaveEventPage,
  ],
  imports: [
    IonicPageModule.forChild(SaveEventPage),
  ],
})
export class SaveEventPageModule {}
