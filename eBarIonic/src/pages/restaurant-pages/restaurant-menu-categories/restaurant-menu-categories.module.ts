import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { RestaurantMenuCategoriesPage } from './restaurant-menu-categories';

@NgModule({
  declarations: [
    RestaurantMenuCategoriesPage,
  ],
  imports: [
    IonicPageModule.forChild(RestaurantMenuCategoriesPage),
  ],
})
export class RestaurantMenuCategoriesPageModule {}
