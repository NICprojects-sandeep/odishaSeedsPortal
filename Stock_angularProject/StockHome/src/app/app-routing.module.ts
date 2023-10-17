import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContainerComponent } from './container/container.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { NearestdealerlistComponent } from './nearestdealerlist/nearestdealerlist.component';
import { OuatComponent } from './ouat/ouat.component';
import { OthersComponent } from './others/others.component';
import { CrriComponent } from './crri/crri.component';
import { ContactusComponent } from './contactus/contactus.component';
import { WebsitepoliciesComponent } from './websitepolicies/websitepolicies.component';

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
    path:'nearestdealerlist',
    component:NearestdealerlistComponent
  },
  {
    path:'contactus',
    component:ContactusComponent
  },
  {
    path:'websitepolicy',
    component:WebsitepoliciesComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{ useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
