import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { RestaurantDetailsPage } from './restaurant-details';

@NgModule({
  declarations: [
    RestaurantDetailsPage,
  ],
  imports: [
    IonicPageModule.forChild(RestaurantDetailsPage),
  ],
})
export class RestaurantDetailsPageModule {}
