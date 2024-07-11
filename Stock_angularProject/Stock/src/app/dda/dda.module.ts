import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DdaRoutingModule } from './dda-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CurrentstockpostionComponent } from './Reports/currentstockpostion/currentstockpostion.component';
import { StatestockpostionComponent } from './Reports/statestockpostion/statestockpostion.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';
import { NgxPrintModule } from 'ngx-print';
import { BlockwisetransctionComponent } from './Reports/blockwisetransction/blockwisetransction.component';
import { DelerwisesaleComponent } from './Reports/delerwisesale/delerwisesale.component';
import { PaymentstatusComponent } from './Reports/paymentstatus/paymentstatus.component';
import { FarmersaleComponent } from './Reports/farmersale/farmersale.component';
import { SeeddistrictdealermprComponent } from './Reports/seeddistrictdealermpr/seeddistrictdealermpr.component';


@NgModule({
  declarations: [
    DashboardComponent,
    CurrentstockpostionComponent,
    StatestockpostionComponent,
    BlockwisetransctionComponent,
    DelerwisesaleComponent,
    PaymentstatusComponent,
    FarmersaleComponent,
    SeeddistrictdealermprComponent
  ],
  imports: [
    CommonModule,
    DdaRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,
    NgxPrintModule,
    NgxSpinnerModule,
  ]
})
export class DdaModule { }
