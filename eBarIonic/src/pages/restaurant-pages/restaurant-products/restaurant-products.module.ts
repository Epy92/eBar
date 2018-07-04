import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { RestaurantProductsPage } from './restaurant-products';

@NgModule({
  declarations: [
    RestaurantProductsPage,
  ],
  imports: [
    IonicPageModule.forChild(RestaurantProductsPage),
  ],
})
export class RestaurantProductsPageModule {}
