import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JrcsRoutingModule } from './jrcs-routing.module';
import { DashboradComponent } from './dashborad/dashborad.component';
import { ErupidashboardComponent } from './erupidashboard/erupidashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';
import { NgxPrintModule } from 'ngx-print';
import { NgxSpinnerModule } from 'ngx-spinner';
import { StatestockpositionComponent } from './statestockposition/statestockposition.component';
import { DealerpacssaleComponent } from './dealerpacssale/dealerpacssale.component';
import { VarietywiseliftComponent } from './varietywiselift/varietywiselift.component';


@NgModule({
  declarations: [
    DashboradComponent,
    ErupidashboardComponent,
    StatestockpositionComponent,
    DealerpacssaleComponent,
    VarietywiseliftComponent
  ],
  imports: [
    CommonModule,
    JrcsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,
    NgxPrintModule,
    NgxSpinnerModule
  ]
})
export class JrcsModule { }
