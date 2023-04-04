import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LeftsidebarComponent } from './leftsidebar/leftsidebar.component';
import { RightsidebarComponent } from './rightsidebar/rightsidebar.component';


@NgModule({
  declarations: [
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    LeftsidebarComponent,
    RightsidebarComponent
  ],
  imports: [
    CommonModule,
    LoginRoutingModule
  ]
})
export class LoginModule { }
