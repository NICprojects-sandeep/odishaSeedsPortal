import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AaoRoutingModule } from './aao-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FarmerdetailscheackComponent } from './farmerdetailscheack/farmerdetailscheack.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    DashboardComponent,
    FarmerdetailscheackComponent
  ],
  imports: [
    CommonModule,
    AaoRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class AaoModule { }
