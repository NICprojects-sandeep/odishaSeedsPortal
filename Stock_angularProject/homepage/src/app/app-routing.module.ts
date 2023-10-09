import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ComponentoneComponent } from './componentone/componentone.component';
import { ContainerComponent } from './container/container.component';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';

const routes: Routes = [
  {
    path:'',
    component:ContainerComponent
  },
  {
    path:'header',
    component:HeaderComponent
  }, 
  {
    path:'footer',
    component:FooterComponent
  },    
      
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
