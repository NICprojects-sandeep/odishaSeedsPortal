import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { SaledeatilsComponent } from './report/saledeatils/saledeatils.component';
import { CurrentstockComponent } from './report/currentstock/currentstock.component';
import { BankstatusComponent } from './report/bankstatus/bankstatus.component';
import { PaymentresponseComponent } from './report/paymentresponse/paymentresponse.component';
import { FarmerinvoiceComponent } from './report/farmerinvoice/farmerinvoice.component';
import { DashboardComponent } from './dashboard/dashboard.component';
const roleObject={ role: 'Dealer'}

const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    data: roleObject,
  },
  {
    path: 'farmersale',
    component: FarmersaleComponent,
    data: roleObject,
  },
  {
    path: 'RPT_DealerSale',
    component: SaledeatilsComponent,
    data: roleObject,
  },
  {
    path: 'RptCurrentStock',
    component: CurrentstockComponent,
    data: roleObject,
  },
  {
    path: 'RptPaymentReq',
    component: PaymentresponseComponent,
    data: roleObject,
  },
  {
    path: 'PaymentDetails',
    component: BankstatusComponent,
    data: roleObject,
  },
  {
    path: 'farmerinvoice/:TRANSACTION_ID',
    component: FarmerinvoiceComponent,
    data: roleObject,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FarmerSaleRoutingModule { }
