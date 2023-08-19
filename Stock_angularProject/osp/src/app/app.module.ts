import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'

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
import { OdiaLayoutComponent } from './odiaComponent/odia-layout/odia-layout.component';
import { OdiaHeaderComponent } from './odiaComponent/odia-header/odia-header.component';
import { OdiaFooterComponent } from './odiaComponent/odia-footer/odia-footer.component';
import { OdiawebsitePolicyComponent } from './odiaComponent/odiawebsite-policy/odiawebsite-policy.component';
import { OdiaContactusComponent } from './odiaComponent/odia-contactus/odia-contactus.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { DialougeboxComponent } from './components/dialougebox/dialougebox.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import { FormsModule } from '@angular/forms';
import { ReleasedVarietyComponent } from './released-variety/released-variety.component';
import { OuatComponent } from './ouat/ouat.component';
import { CrriComponent } from './crri/crri.component';
import { ModalContentComponent } from './modal-content/modal-content.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GraphModalComponent } from './graph-modal/graph-modal.component';
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
    WebsitePolicyComponent,
    OdiaLayoutComponent,
    OdiaHeaderComponent,
    OdiaFooterComponent,
    OdiawebsitePolicyComponent,
    OdiaContactusComponent,
    DialougeboxComponent,
    ReleasedVarietyComponent,
    OuatComponent,
    CrriComponent,
    ModalContentComponent,
    GraphModalComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    IndexModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDialogModule,
    BrowserAnimationsModule,
    FormsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
