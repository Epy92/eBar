import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { RestaurantPhotosPage } from './restaurant-photos';

@NgModule({
  declarations: [
    RestaurantPhotosPage,
  ],
  imports: [
    IonicPageModule.forChild(RestaurantPhotosPage),
  ],
})
export class RestaurantPhotosPageModule {}
