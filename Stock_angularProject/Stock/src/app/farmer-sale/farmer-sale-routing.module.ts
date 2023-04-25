import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { SaledeatilsComponent } from './report/saledeatils/saledeatils.component';

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
    component: FarmersaleComponent,
  },
  {
    path: 'farmersale',
    component: FarmersaleComponent,
  },
  {
    path: 'farmersale',
    component: FarmersaleComponent,
  },
  {
    path: 'farmersale',
    component: FarmersaleComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FarmerSaleRoutingModule { }
