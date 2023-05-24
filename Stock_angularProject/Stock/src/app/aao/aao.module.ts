import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AaoRoutingModule } from './aao-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FarmerdetailscheackComponent } from './farmerdetailscheack/farmerdetailscheack.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FarmerTypeUpdateComponent } from './farmer-type-update/farmer-type-update.component';

@NgModule({
  declarations: [
    DashboardComponent,
    FarmerdetailscheackComponent,
    FarmerTypeUpdateComponent
  ],
  imports: [
    CommonModule,
    AaoRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class AaoModule { }
