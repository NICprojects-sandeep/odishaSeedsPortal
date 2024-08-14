import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContainerComponent } from './container/container.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { OuatComponent } from './ouat/ouat.component';
import { OthersComponent } from './others/others.component';
import { CrriComponent } from './crri/crri.component';
import { ContactusComponent } from './contactus/contactus.component';
import { WebsitepoliciesComponent } from './websitepolicies/websitepolicies.component';
import { VarietysearchComponent } from './varietysearch/varietysearch.component';
import { OdiacontainerComponent } from './odia/odiacontainer/odiacontainer.component';
import { OdiawebsitepoliciesComponent } from './odia/odiawebsitepolicies/odiawebsitepolicies.component';
import { OdiacontactusComponent } from './odia/odiacontactus/odiacontactus.component';
import { OdiaouatComponent } from './odia/odiaouat/odiaouat.component';
import { OdiacrriComponent } from './odia/odiacrri/odiacrri.component';
import { OdiaothersComponent } from './odia/odiaothers/odiaothers.component';

const routes: Routes = [
  {
    path:'',
    component:ContainerComponent
  },
  {
    path:'ouat',
    component:OuatComponent
  }, 
  {
    path:'crri',
    component:CrriComponent
  }, 
  {
    path:'others',
    component:OthersComponent
  },
  {
    path:'contactus',
    component:ContactusComponent
  },
  {
    path:'websitepolicy',
    component:WebsitepoliciesComponent
  },
  {
    path:'VarietySearch',
    component:VarietysearchComponent
  },
  {
    path:'odia',
    component:OdiacontainerComponent
  },
  {
    path:'odiawebsitepolicy',
    component:OdiawebsitepoliciesComponent
  },
  {
    path:'odiacontactus',
    component:OdiacontactusComponent
  },
  {
    path:'odiaouat',
    component:OdiaouatComponent
  }, 
  {
    path:'odiacrri',
    component:OdiacrriComponent
  }, 
  {
    path:'odiaothers',
    component:OdiaothersComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{ useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
