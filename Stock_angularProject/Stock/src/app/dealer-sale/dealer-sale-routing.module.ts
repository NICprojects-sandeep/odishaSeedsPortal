import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockSaleEntryComponent } from './stock-sale-entry/stock-sale-entry.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'Stock_SaleEntry',
    pathMatch: 'full'
  },
  {
    path: 'Stock_SaleEntry',
    component: StockSaleEntryComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DealerSaleRoutingModule { }
