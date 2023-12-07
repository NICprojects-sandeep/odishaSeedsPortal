import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { SaledeatilsComponent } from './report/saledeatils/saledeatils.component';
import { CurrentstockComponent } from './report/currentstock/currentstock.component';
import { BankstatusComponent } from './report/bankstatus/bankstatus.component';
import { PaymentresponseComponent } from './report/paymentresponse/paymentresponse.component';
import { FarmerinvoiceComponent } from './report/farmerinvoice/farmerinvoice.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FarmerpaymentstatusComponent } from './report/farmerpaymentstatus/farmerpaymentstatus.component';
import { DistWiseCurrentStockComponent } from './report/dist-wise-current-stock/dist-wise-current-stock.component';
import { UpdateAccDtlsComponent } from './report/update-acc-dtls/update-acc-dtls.component';
import { PrebookingdetailsComponent } from './report/prebookingdetails/prebookingdetails.component';
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
  },
  {
    path: 'FarmerPaymentStatus',
    component: FarmerpaymentstatusComponent,
    data: roleObject,
  },
  {
    path: 'distwisecurrentstatus',
    component: DistWiseCurrentStockComponent,
    data: roleObject,
  },
  {
    path: 'UpdateAccDtls',
    component: UpdateAccDtlsComponent,
    data: roleObject,
  },
  {
    path: 'prebookingdetails',
    component: PrebookingdetailsComponent,
    data: roleObject,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FarmerSaleRoutingModule { }
