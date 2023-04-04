import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChartVarietyListComponent } from 'src/app/components/chart-variety-list/chart-variety-list.component';
import { ChartLicenceListComponent } from 'src/app/components/chart-licence-list/chart-licence-list.component';
import { LayoutComponent } from 'src/app/components/layout/layout.component';

const routes: Routes = [
  {
    path:'',
    component:LayoutComponent
  },  
  {
    path:'chartVariety',
    component:ChartVarietyListComponent
  },
  {
    path:'chartLicence',
    component:ChartLicenceListComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
