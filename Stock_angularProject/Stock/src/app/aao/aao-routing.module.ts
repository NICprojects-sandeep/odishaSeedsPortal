import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FarmerdetailscheackComponent } from './farmerdetailscheack/farmerdetailscheack.component';
import { DashboardComponent } from './dashboard/dashboard.component';
const roleObject = { role: 'AAOO' }
const routes: Routes = [
  {
    path: '',
    redirectTo: 'farmerdetailscheck',
    pathMatch: 'full'
  },
  {
    path: 'farmerdetailscheck',
    component: FarmerdetailscheackComponent, 
     data: roleObject
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    data: roleObject
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AaoRoutingModule { }
