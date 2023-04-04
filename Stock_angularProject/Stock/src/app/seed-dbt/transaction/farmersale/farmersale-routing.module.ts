import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { FarmerinfoComponent } from './farmersale/farmerinfo/farmerinfo.component';
import { FarmerdtlComponent } from './farmersale/farmerdtl/farmerdtl.component';
import { DealerstockdtlComponent } from './farmersale/dealerstockdtl/dealerstockdtl.component';
import { LayoutComponent } from '../../../layout/layout.component';


const routes: Routes = [
  { path: "", redirectTo: "Farmersale", pathMatch: "full" },
  { path: "Farmersale", component: FarmersaleComponent },
  { path: "Farmerinfo", component: FarmerinfoComponent },
  { path: "Farmerdtl", component: FarmerdtlComponent },
  { path: "Dealerstockdtl", component: DealerstockdtlComponent },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FarmersaleRoutingModule { }
