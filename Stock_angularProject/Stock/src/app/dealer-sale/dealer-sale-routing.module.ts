import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockSaleEntryComponent } from './stock-sale-entry/stock-sale-entry.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CashmemodetailsComponent } from './reports/cashmemodetails/cashmemodetails.component';
import { OssclotentryComponent } from './ossclotentry/ossclotentry.component';
import { GodownwisestockComponent } from './godownwisestock/godownwisestock.component';
const roleObject={ role: 'SPO'}
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
  }  ,
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
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DealerSaleRoutingModule { }
