import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CashmemoTranscationdeletedComponent } from './cashmemo-transcationdeleted/cashmemo-transcationdeleted.component';
import { PrebookingcanceledComponent } from './prebookingcanceled/prebookingcanceled.component';
import { PrebookingdashboardComponent } from './prebookingdashboard/prebookingdashboard.component';
import { SeedsubsidyforgroundnutComponent } from './seedsubsidyforgroundnut/seedsubsidyforgroundnut.component';
import { TranscationdetailsdeletedComponent } from './transcationdetailsdeleted/transcationdetailsdeleted.component';
import { TransferOfSeedSubsidyForGroundnutComponent } from './transfer-of-seed-subsidy-for-groundnut/transfer-of-seed-subsidy-for-groundnut.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'transferOfSeedSubsidy',
    pathMatch: 'full'
  },
  {
    path: 'transferOfSeedSubsidy',
    component: TransferOfSeedSubsidyForGroundnutComponent,
  },
  {
    path: 'Seedsubsidyforgroundnut',
    component: SeedsubsidyforgroundnutComponent,
  }, {
    path: 'transctiondetailsdeleted',
    component: TranscationdetailsdeletedComponent,
  },{
    path: 'cashmemotransctiondetailsdeleted',
    component: CashmemoTranscationdeletedComponent,
  },{
    path: 'prebookingdashboard',
    component: PrebookingdashboardComponent,
  },
  {
    path: 'prebookingdatacanceled',
    component: PrebookingcanceledComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
