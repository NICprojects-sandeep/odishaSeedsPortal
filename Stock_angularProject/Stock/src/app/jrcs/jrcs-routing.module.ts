import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboradComponent } from './dashborad/dashborad.component';
import { ErupidashboardComponent } from './erupidashboard/erupidashboard.component';
import { StatestockpositionComponent } from './statestockposition/statestockposition.component';
import { DealerpacssaleComponent } from './dealerpacssale/dealerpacssale.component';
import { VarietywiseliftComponent } from './varietywiselift/varietywiselift.component';

const roleObject={ role: 'jrcs'}
const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: DashboradComponent,
    data: roleObject,
  },
  {
    path: 'erupidashboard',
    component: ErupidashboardComponent,
    data: roleObject,
  },
  {
    path: 'statestockposition',
    component: StatestockpositionComponent,
    data: roleObject,
  },{
    path: 'varietywiselift',
    component: VarietywiseliftComponent,
    data: roleObject,
  },
  {
    path: 'dealerpacssale',
    component: DealerpacssaleComponent,
    data: roleObject,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class JrcsRoutingModule { }
