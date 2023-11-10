import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockSaleEntryComponent } from './stock-sale-entry/stock-sale-entry.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CashmemodetailsComponent } from './reports/cashmemodetails/cashmemodetails.component';
import { OssclotentryComponent } from './ossclotentry/ossclotentry.component';
import { ReceivedetailsComponent } from './reports/receivedetails/receivedetails.component';
import { GodownwisestockComponent } from './reports/godownwisestock/godownwisestock.component';
import { DatewisesaledetailsComponent } from './reports/datewisesaledetails/datewisesaledetails.component';
import { DatewisegodowntransferdetailsComponent } from './reports/datewisegodowntransferdetails/datewisegodowntransferdetails.component';
import { SaledetailsComponent } from './reports/saledetails/saledetails.component';
import { FarmerpaymentstatuscheckComponent } from './reports/farmerpaymentstatuscheck/farmerpaymentstatuscheck.component';
import { GroundnutDemonstrationComponent } from './groundnut-demonstration/groundnut-demonstration.component';
import { StockreceiveentryComponent } from './stockreceiveentry/stockreceiveentry.component';
const roleObject = { role: 'SPO' }
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
    path: 'Stock_SaleEntry',
    component: StockSaleEntryComponent,
    data: roleObject,
  },
  {
    path: 'cashmemodetails',
    component: CashmemodetailsComponent,
    data: roleObject,
  },
  {
    path: 'ossclotentry',
    component: OssclotentryComponent,
    data: roleObject,
  },
  {
    path: 'godownwisestock',
    component: GodownwisestockComponent,
    data: roleObject,
  },
  {
    path: 'receivedetails',
    component: ReceivedetailsComponent,
    data: roleObject,
  },
  {
    path: 'DatewiseSaleDetails',
    component: DatewisesaledetailsComponent,
    data: roleObject,
  },
  {
    path: 'DatewiseGoDownTransferDetails',
    component: DatewisegodowntransferdetailsComponent,
    data: roleObject,
  },
  {
    path: 'saledetails',
    component: SaledetailsComponent,
    data: roleObject,
  },
  {
    path: 'farmerpaymentstatus',
    component: FarmerpaymentstatuscheckComponent,
    data: roleObject,
  },
  {
    path: 'groundnutDemonstration',
    component: GroundnutDemonstrationComponent,
    data: roleObject,
  },
  {
    path: 'Stock_ReceiveEntry',
    component: StockreceiveentryComponent,
    data: roleObject,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DealerSaleRoutingModule { }
