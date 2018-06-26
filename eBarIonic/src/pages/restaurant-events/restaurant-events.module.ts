import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { RestaurantEventsPage } from './restaurant-events';

@NgModule({
  declarations: [
    RestaurantEventsPage,
  ],
  imports: [
    IonicPageModule.forChild(RestaurantEventsPage),
  ],
})
export class RestaurantEventsPageModule {}
