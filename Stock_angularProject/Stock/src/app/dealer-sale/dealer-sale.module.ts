import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DealerSaleRoutingModule } from './dealer-sale-routing.module';
import { StockSaleEntryComponent } from './stock-sale-entry/stock-sale-entry.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgxPrintModule } from 'ngx-print';
import { CashmemodetailsComponent } from './reports/cashmemodetails/cashmemodetails.component';
import { OssclotentryComponent } from './ossclotentry/ossclotentry.component';
import { GodownwisestockComponent } from './reports/godownwisestock/godownwisestock.component';
import { SaledetailsComponent } from './reports/saledetails/saledetails.component';
import { DatewisegodowntransferdetailsComponent } from './reports/datewisegodowntransferdetails/datewisegodowntransferdetails.component';
import { DatewisesaledetailsComponent } from './reports/datewisesaledetails/datewisesaledetails.component';
import { ReceivedetailsComponent } from './reports/receivedetails/receivedetails.component';
import { FarmerpaymentstatuscheckComponent } from './reports/farmerpaymentstatuscheck/farmerpaymentstatuscheck.component';
import { GroundnutDemonstrationComponent } from './groundnut-demonstration/groundnut-demonstration.component';
import { StockreceiveentryComponent } from './stockreceiveentry/stockreceiveentry.component';

@NgModule({
  declarations: [
    StockSaleEntryComponent,
    DashboardComponent,
    CashmemodetailsComponent,
    OssclotentryComponent,
    GodownwisestockComponent,
    SaledetailsComponent,
    DatewisegodowntransferdetailsComponent,
    DatewisesaledetailsComponent,
    ReceivedetailsComponent,
    FarmerpaymentstatuscheckComponent,
    GroundnutDemonstrationComponent,
    StockreceiveentryComponent
  ],
  imports: [
    CommonModule,
    DealerSaleRoutingModule,
    NgxSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPrintModule
  ]
})
export class DealerSaleModule { }
