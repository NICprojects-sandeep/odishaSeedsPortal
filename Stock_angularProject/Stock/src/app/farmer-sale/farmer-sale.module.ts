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
import { DistWiseCurrentStockComponent } from './report/dist-wise-current-stock/dist-wise-current-stock.component';
import { FarmerpaymentstatusComponent } from './report/farmerpaymentstatus/farmerpaymentstatus.component';
import { UpdateAccDtlsComponent } from './report/update-acc-dtls/update-acc-dtls.component';
import { PrebookingdetailsComponent } from './report/prebookingdetails/prebookingdetails.component';

@NgModule({
  declarations: [
    FarmersaleComponent,
    SaledeatilsComponent,
    CurrentstockComponent,
    PaymentresponseComponent,
    BankstatusComponent,
    FarmerinvoiceComponent,
    DashboardComponent,
    DistWiseCurrentStockComponent,
    FarmerpaymentstatusComponent,
    UpdateAccDtlsComponent,
    PrebookingdetailsComponent
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
