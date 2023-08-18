import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DealerSaleRoutingModule } from './dealer-sale-routing.module';
import { StockSaleEntryComponent } from './stock-sale-entry/stock-sale-entry.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgxPrintModule } from 'ngx-print';
import { CashmemodetailsComponent } from './reports/cashmemodetails/cashmemodetails.component';

@NgModule({
  declarations: [
    StockSaleEntryComponent,
    DashboardComponent,
    CashmemodetailsComponent
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
