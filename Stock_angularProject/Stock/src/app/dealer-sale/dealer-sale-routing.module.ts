import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockSaleEntryComponent } from './stock-sale-entry/stock-sale-entry.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CashmemoreportsComponent } from './cashmemoreports/cashmemoreports.component';
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
    component: CashmemoreportsComponent,
    data: roleObject,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DealerSaleRoutingModule { }
