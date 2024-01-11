import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { ContainerComponent } from './container/container.component';
import { ComponentoneComponent } from './componentone/componentone.component';
import { SeedsupportComponent } from './seedsupport/seedsupport.component';
import { FeestructureComponent } from './feestructure/feestructure.component';
import { DialougeboxComponent } from './dialougebox/dialougebox.component';
import { GraphModalComponent } from './graph-modal/graph-modal.component';
import { DealermodalComponent } from './dealermodal/dealermodal.component';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OuatComponent } from './ouat/ouat.component';
import { CrriComponent } from './crri/crri.component';
import { OthersComponent } from './others/others.component';
import { ContactusComponent } from './contactus/contactus.component';
import { WebsitepoliciesComponent } from './websitepolicies/websitepolicies.component';
import { BackButtonDisableModule } from 'angular-disable-browser-back-button';
import { HomepagesliderComponent } from './homepageslider/homepageslider.component';
import { MinisterimagesComponent } from './ministerimages/ministerimages.component';
import { ModimilletsComponent } from './container/modimillets/modimillets.component';
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    ContainerComponent,
    ComponentoneComponent,
    SeedsupportComponent,
    FeestructureComponent,
    DialougeboxComponent,
    GraphModalComponent,
    DealermodalComponent,
    OuatComponent,
    CrriComponent,
    OthersComponent,
    ContactusComponent,
    WebsitepoliciesComponent,
    HomepagesliderComponent,
    MinisterimagesComponent,
    ModimilletsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatDialogModule,
    NgbModule,
    BrowserAnimationsModule,
    
    BackButtonDisableModule.forRoot({
      preserveScrollPosition: true
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
