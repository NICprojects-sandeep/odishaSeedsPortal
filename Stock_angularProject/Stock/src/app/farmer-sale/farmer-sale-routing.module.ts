import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { SaledeatilsComponent } from './report/saledeatils/saledeatils.component';
import { CurrentstockComponent } from './report/currentstock/currentstock.component';
import { BankstatusComponent } from './report/bankstatus/bankstatus.component';
import { PaymentresponseComponent } from './report/paymentresponse/paymentresponse.component';
import { FarmerinvoiceComponent } from './report/farmerinvoice/farmerinvoice.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'farmersale',
    pathMatch: 'full'
  },
  {
    path: 'farmersale',
    component: FarmersaleComponent,
  },
  {
    path: 'RPT_DealerSale',
    component: SaledeatilsComponent,
  },
  {
    path: 'RptCurrentStock',
    component: CurrentstockComponent,
  },
  {
    path: 'RptPaymentReq',
    component: PaymentresponseComponent,
  },
  {
    path: 'PaymentDetails',
    component: BankstatusComponent,
  },
  {
    path: 'farmerinvoice/:TRANSACTION_ID',
    component: FarmerinvoiceComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FarmerSaleRoutingModule { }
