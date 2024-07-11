import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CurrentstockpostionComponent } from './Reports/currentstockpostion/currentstockpostion.component';
import { StatestockpostionComponent } from './Reports/statestockpostion/statestockpostion.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BlockwisetransctionComponent } from './Reports/blockwisetransction/blockwisetransction.component';
import { DelerwisesaleComponent } from './Reports/delerwisesale/delerwisesale.component';
import { PaymentstatusComponent } from './Reports/paymentstatus/paymentstatus.component';
import { FarmersaleComponent } from './Reports/farmersale/farmersale.component';
import { SeeddistrictdealermprComponent } from './Reports/seeddistrictdealermpr/seeddistrictdealermpr.component';


const roleObject = { role: 'DIST' }
const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: DashboardComponent, 
     data: roleObject
  },
  {
    path: 'currentstockposition',
    component: CurrentstockpostionComponent, 
     data: roleObject
  },
  {
    path: 'statestockpostion',
    component: StatestockpostionComponent,
    data: roleObject
  },
  {
    path: 'blockwisetransction',
    component: BlockwisetransctionComponent,
    data: roleObject
  },
  {
    path: 'delerwisesale',
    component: DelerwisesaleComponent,
    data: roleObject
  },
  {
    path: 'paymentstatus',
    component: PaymentstatusComponent,
    data: roleObject
  },
  {
    path: 'farmersale',
    component: FarmersaleComponent,
    data: roleObject
  },
  {
    path: 'seeddistrictdealermpr',
    component: SeeddistrictdealermprComponent,
    data: roleObject
  }];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DdaRoutingModule { }
