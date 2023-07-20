import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FarmerSaleRoutingModule } from './farmer-sale-routing.module';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';
import { NgxPrintModule } from 'ngx-print';
import { SaledeatilsComponent } from './report/saledeatils/saledeatils.component';
import { CurrentstockComponent } from './report/currentstock/currentstock.component';
import { PaymentresponseComponent } from './report/paymentresponse/paymentresponse.component';
import { BankstatusComponent } from './report/bankstatus/bankstatus.component';
import { FarmerinvoiceComponent } from './report/farmerinvoice/farmerinvoice.component';

import { NgxSpinnerModule } from 'ngx-spinner';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    FarmersaleComponent,
    SaledeatilsComponent,
    CurrentstockComponent,
    PaymentresponseComponent,
    BankstatusComponent,
    FarmerinvoiceComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    FarmerSaleRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,
    NgxPrintModule,
    NgxSpinnerModule,
  ]
})
export class FarmerSaleModule { }
