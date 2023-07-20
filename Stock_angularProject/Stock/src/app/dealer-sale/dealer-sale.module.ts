import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DealerSaleRoutingModule } from './dealer-sale-routing.module';
import { StockSaleEntryComponent } from './stock-sale-entry/stock-sale-entry.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    StockSaleEntryComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    DealerSaleRoutingModule,
    NgxSpinnerModule,
    FormsModule
  ]
})
export class DealerSaleModule { }
