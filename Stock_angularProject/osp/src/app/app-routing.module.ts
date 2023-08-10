import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChartVarietyListComponent } from 'src/app/components/chart-variety-list/chart-variety-list.component';
import { ChartLicenceListComponent } from 'src/app/components/chart-licence-list/chart-licence-list.component';
import { LayoutComponent } from 'src/app/components/layout/layout.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { WebsitePolicyComponent } from './website-policy/website-policy.component';
import { OdiaLayoutComponent } from './odiaComponent/odia-layout/odia-layout.component';
import { OdiaContactusComponent } from './odiaComponent/odia-contactus/odia-contactus.component';
import { OdiawebsitePolicyComponent } from './odiaComponent/odiawebsite-policy/odiawebsite-policy.component';
import { ReleasedVarietyComponent } from './released-variety/released-variety.component';
import { OuatComponent } from './ouat/ouat.component';
import { CrriComponent } from './crri/crri.component';

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
  {
    path:'contactUs',
    component:ContactUsComponent
  },
  {
    path:'websitepolicy',
    component:WebsitePolicyComponent
  },
  {
    path:'odia',
    component:OdiaLayoutComponent
  },
  {
    path:'odiacontactUs',
    component:OdiaContactusComponent
  },
  {
    path:'odiawebsitepolicy',
    component:OdiawebsitePolicyComponent
  }, 
  {
    path:'released',
    component:ReleasedVarietyComponent
  }, 
  {
    path:'ouat',
    component:OuatComponent
  },
  {
    path:'crri',
    component:CrriComponent
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
