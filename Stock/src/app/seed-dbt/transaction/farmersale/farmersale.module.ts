import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FarmersaleRoutingModule } from './farmersale-routing.module';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { FarmerinfoComponent } from './farmersale/farmerinfo/farmerinfo.component';
import { FarmerdtlComponent } from './farmersale/farmerdtl/farmerdtl.component';
import { DealerstockdtlComponent } from './farmersale/dealerstockdtl/dealerstockdtl.component';
import { FormsModule } from '@angular/forms';
import { InvoiceComponent } from './farmersale/invoice/invoice.component';


@NgModule({
  declarations: [
    FarmersaleComponent,
    FarmerinfoComponent,
    FarmerdtlComponent,
    DealerstockdtlComponent,
    InvoiceComponent
  ],
  imports: [
    CommonModule,
    FarmersaleRoutingModule,
    FormsModule
  ]
})
export class FarmersaleModule { }
