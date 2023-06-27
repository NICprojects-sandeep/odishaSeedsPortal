import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {RouterModule} from '@angular/router'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { IndexModule } from './index/index.module';
import { HeaderComponent } from './components/header/header.component';
import { NewsComponent } from './components/news/news.component';
import { ContainerSec1Component } from './components/container-sec1/container-sec1.component';
import { ContainerSec2Component } from './components/container-sec2/container-sec2.component';
import { ContainerSec3Component } from './components/container-sec3/container-sec3.component';
import { ContainerSec4Component } from './components/container-sec4/container-sec4.component';
import { ContainerSec5Component } from './components/container-sec5/container-sec5.component';
import { ContainerSec6Component } from './components/container-sec6/container-sec6.component';
import { LayoutComponent } from './components/layout/layout.component';
import { ChartVarietyListComponent } from './components/chart-variety-list/chart-variety-list.component';
import { ChartLicenceListComponent } from './components/chart-licence-list/chart-licence-list.component';
import { FooterComponent } from './components/footer/footer.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { WebsitePolicyComponent } from './website-policy/website-policy.component';

@NgModule({
  declarations: [    
    AppComponent,
    HeaderComponent,
    NewsComponent,
    ContainerSec1Component,
    ContainerSec2Component,
    ContainerSec3Component,
    ContainerSec4Component,
    ContainerSec5Component,
    ContainerSec6Component,
    LayoutComponent,
    ChartVarietyListComponent,
    ChartLicenceListComponent,
    FooterComponent,
    ContactUsComponent,
    WebsitePolicyComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    IndexModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
