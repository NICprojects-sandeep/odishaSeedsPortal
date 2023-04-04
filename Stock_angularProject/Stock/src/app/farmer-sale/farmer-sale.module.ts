import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FarmerSaleRoutingModule } from './farmer-sale-routing.module';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    FarmersaleComponent
  ],
  imports: [
    CommonModule,
    FarmerSaleRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,
  ]
})
export class FarmerSaleModule { }
