import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AaoRoutingModule } from './aao-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FarmerdetailscheackComponent } from './farmerdetailscheack/farmerdetailscheack.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FarmerTypeUpdateComponent } from './farmer-type-update/farmer-type-update.component';

import { NgxPrintModule } from 'ngx-print';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { PhysicalSaleEntryComponent } from './physical-sale-entry/physical-sale-entry.component';
import { UpdationofZeroTransactionComponent } from './updationof-zero-transaction/updationof-zero-transaction.component';
import { FarmerDetailsUpdationComponent } from './farmer-details-updation/farmer-details-updation.component';
@NgModule({
  declarations: [
    DashboardComponent,
    FarmerdetailscheackComponent,
    FarmerTypeUpdateComponent,
    PhysicalSaleEntryComponent,
    UpdationofZeroTransactionComponent,
    FarmerDetailsUpdationComponent
  ],
  imports: [
    CommonModule,
    AaoRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPrintModule,
    MatSelectModule,
    MatFormFieldModule,
  ]
})
export class AaoModule { }
