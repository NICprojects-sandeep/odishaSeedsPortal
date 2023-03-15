import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { TransferOfSeedSubsidyForGroundnutComponent } from './transfer-of-seed-subsidy-for-groundnut/transfer-of-seed-subsidy-for-groundnut.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { PubliclayoutComponent } from './publiclayout/publiclayout.component';
import { PublicfooterComponent } from './publiclayout/publicfooter/publicfooter.component';
import { PublicheaderComponent } from './publiclayout/publicheader/publicheader.component';
import { PublicleftsidebarComponent } from './publiclayout/publicleftsidebar/publicleftsidebar.component';
import { PublicrightsidebarComponent } from './publiclayout/publicrightsidebar/publicrightsidebar.component';
import { SeedsubsidyforgroundnutComponent } from './seedsubsidyforgroundnut/seedsubsidyforgroundnut.component';
import { TranscationdetailsdeletedComponent } from './transcationdetailsdeleted/transcationdetailsdeleted.component';
import { PrebookingdashboardComponent } from './prebookingdashboard/prebookingdashboard.component';
import { PrebookingcanceledComponent } from './prebookingcanceled/prebookingcanceled.component';
import { CashmemoTranscationdeletedComponent } from './cashmemo-transcationdeleted/cashmemo-transcationdeleted.component';
import { FilterPipe } from './filter.pipe';
import { SumPipe } from './sum.pipe';
import { SeedliveInventoryComponent } from './seedlive-inventory/seedlive-inventory.component';
import { IncentiveComponent } from './incentive/incentive.component';

@NgModule({
  declarations: [
    TransferOfSeedSubsidyForGroundnutComponent,
    PubliclayoutComponent,
    PublicfooterComponent,
    PublicheaderComponent,
    PublicleftsidebarComponent,
    PublicrightsidebarComponent,
    SeedsubsidyforgroundnutComponent,
    TranscationdetailsdeletedComponent,
    PrebookingdashboardComponent,
    PrebookingcanceledComponent,
    CashmemoTranscationdeletedComponent,
    FilterPipe,
    SumPipe,
    SeedliveInventoryComponent,
    IncentiveComponent
  ],
  imports: [
    CommonModule,
    // BrowserModule,
    PublicRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    
  ]
})
export class PublicModule { }
