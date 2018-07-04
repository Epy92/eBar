import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { RestaurantEmployeesPage } from './restaurant-employees';

@NgModule({
  declarations: [
    RestaurantEmployeesPage,
  ],
  imports: [
    IonicPageModule.forChild(RestaurantEmployeesPage),
  ],
})
export class RestaurantEmployeesPageModule {}
