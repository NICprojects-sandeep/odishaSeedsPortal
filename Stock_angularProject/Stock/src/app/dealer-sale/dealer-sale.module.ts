import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DealerSaleRoutingModule } from './dealer-sale-routing.module';
import { StockSaleEntryComponent } from './stock-sale-entry/stock-sale-entry.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgxPrintModule } from 'ngx-print';
import { CashmemodetailsComponent } from './reports/cashmemodetails/cashmemodetails.component';
import { OssclotentryComponent } from './ossclotentry/ossclotentry.component';
import { GodownwisestockComponent } from './reports/godownwisestock/godownwisestock.component';
import { ReceiveDetailsComponent } from './receive-details/receive-details.component';
import { DatewiseSaleDetailsComponent } from './datewise-sale-details/datewise-sale-details.component';
import { DatewiseGoDownTransferDetailsComponent } from './datewise-go-down-transfer-details/datewise-go-down-transfer-details.component';
import { SaledetailsComponent } from './reports/saledetails/saledetails.component';
import { DatewisegodowntransferdetailsComponent } from './reports/datewisegodowntransferdetails/datewisegodowntransferdetails.component';
import { DatewisesaledetailsComponent } from './reports/datewisesaledetails/datewisesaledetails.component';
import { ReceivedetailsComponent } from './reports/receivedetails/receivedetails.component';

@NgModule({
  declarations: [
    StockSaleEntryComponent,
    DashboardComponent,
    CashmemodetailsComponent,
    OssclotentryComponent,
    GodownwisestockComponent,
    ReceiveDetailsComponent,
    DatewiseSaleDetailsComponent,
    DatewiseGoDownTransferDetailsComponent,
    SaledetailsComponent,
    DatewisegodowntransferdetailsComponent,
    DatewisesaledetailsComponent,
    ReceivedetailsComponent
  ],
  imports: [
    CommonModule,
    DealerSaleRoutingModule,
    NgxSpinnerModule,
    FormsModule,
    NgxPrintModule
  ]
})
export class DealerSaleModule { }
