import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';

import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { LeftsidebarComponent } from './layout/leftsidebar/leftsidebar.component';
import { RightsidebarComponent } from './layout/rightsidebar/rightsidebar.component';
import { LayoutComponent } from './layout/layout.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { StocksaleComponent } from './stocksale/stocksale.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule, DatePipe } from '@angular/common';
import { BackButtonDisableModule } from 'angular-disable-browser-back-button';

import { ToastrModule } from 'ngx-toastr';
import {NgxPrintModule} from 'ngx-print';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LeftsidebarComponent,
    RightsidebarComponent,
    LayoutComponent,
    routingComponents,
    FarmersaleComponent,
    StocksaleComponent,
    
  ],
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDialogModule,
    NgxPrintModule,
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPrintModule,
    // ToastrModule.forRoot({
    //   closeButton: true,
    //   timeOut: 10000,
    //   extendedTimeOut: 10000,
    //   enableHtml: true,
    
    BrowserAnimationsModule,
    //   progressBar: true
    // }),
    // ToastrModule.forRoot({
    //   closeButton: true,
    //   timeOut: 10000,
    //   extendedTimeOut: 10000,
    //   enableHtml: true,
    //   progressBar: true
    // }),
    ToastrModule.forRoot({
      closeButton: true,
      timeOut: 10000, 
      extendedTimeOut: 10000,
      enableHtml: true,
      progressBar: true
    }),
    BackButtonDisableModule.forRoot({
      preserveScrollPosition: true
    }),
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
