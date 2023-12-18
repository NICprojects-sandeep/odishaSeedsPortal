import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CurrentstockpositionComponent } from './reports/currentstockposition/currentstockposition.component';
import { StatestockpositionComponent } from './reports/statestockposition/statestockposition.component';
import { VarietywiseliftComponent } from './reports/varietywiselift/varietywiselift.component';
import { VarietysearchComponent } from './reports/varietysearch/varietysearch.component';
import { DealerpacssaleComponent } from './reports/dealerpacssale/dealerpacssale.component';
import { ComponentwisesubsidySalecomponentComponent } from './reports/componentwisesubsidy-salecomponent/componentwisesubsidy-salecomponent.component';
import { PaymentdetailsComponent } from './reports/paymentdetails/paymentdetails.component';
import { DistwisePACSDetailsComponent } from './reports/distwise-pacsdetails/distwise-pacsdetails.component';
import { SchemeWiseConsolidatedComponent } from './reports/scheme-wise-consolidated/scheme-wise-consolidated.component';
import { InputSubsidyComponent } from './reports/input-subsidy/input-subsidy.component';
import { SchemewiseSubsidyUtilisedComponent } from './reports/schemewise-subsidy-utilised/schemewise-subsidy-utilised.component';
import { SchemeWiseVaraityAgeWiseComponent } from './reports/scheme-wise-varaity-age-wise/scheme-wise-varaity-age-wise.component';
import { FoundationtoCertifiedLotsComponent } from './reports/foundationto-certified-lots/foundationto-certified-lots.component';
import { DistWiseCropAgeWiseSeedDataComponent } from './reports/dist-wise-crop-age-wise-seed-data/dist-wise-crop-age-wise-seed-data.component';
import { DealerpacspaymentComponent } from './reports/dealerpacspayment/dealerpacspayment.component';
import { DealerPACSSaleDetailsComponent } from './reports/dealer-pacssale-details/dealer-pacssale-details.component';
import { FarmersaleComponent } from './reports/farmersale/farmersale.component';
import { FarmerdetailscheckComponent } from './reports/farmerdetailscheck/farmerdetailscheck.component';
import { DailyProgressReportComponent } from './reports/daily-progress-report/daily-progress-report.component';
const roleObject={ role: 'ADMI'}
const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    data: roleObject,
  },
  {
    path: 'currentstockposition',
    component: CurrentstockpositionComponent,
    data: roleObject,
  },
  {
    path: 'statestockposition',
    component: StatestockpositionComponent,
    data: roleObject,
  },
  {
    path: 'varietywiselift',
    component: VarietywiseliftComponent,
    data: roleObject,
  },
  {
    path: 'varietywisesearch',
    component: VarietysearchComponent,
    data: roleObject,
  },
  {
    path: 'farmersale',
    component: FarmersaleComponent,
    data: roleObject,
  },
  {
    path: 'dealerpacssale',
    component: DealerpacssaleComponent,
    data: roleObject,
  },
  {
    path: 'componentwisesubsidysale',
    component: ComponentwisesubsidySalecomponentComponent,
    data: roleObject,
  },
  {
    path: 'paymentdetails',
    component: PaymentdetailsComponent,
    data: roleObject,
  },
  {
    path: 'distwisePACSdetails',
    component: DistwisePACSDetailsComponent,
    data: roleObject,
  },
  {
    path: 'schemewiseconsolidated',
    component: SchemeWiseConsolidatedComponent,
    data: roleObject,
  },
  {
    path: 'inputsusidy',
    component: InputSubsidyComponent,
    data: roleObject,
  },
  {
    path: 'schemewisesubsidyutilised',
    component: SchemewiseSubsidyUtilisedComponent,
    data: roleObject,
  },
  {
    path: 'schemeWisevaraityagewise',
    component: SchemeWiseVaraityAgeWiseComponent,
    data: roleObject,
  },
  {
    path: 'foundationtocerified',
    component: FoundationtoCertifiedLotsComponent,
    data: roleObject,
  },
  {
    path: 'distwisecropagewiseseeddata',
    component: DistWiseCropAgeWiseSeedDataComponent,
    data: roleObject,
  },
  {
    path: 'dealerpacspayment',
    component: DealerpacspaymentComponent,
    data: roleObject,
  },
  {
    path: 'dealerpacssale',
    component: DealerPACSSaleDetailsComponent,
    data: roleObject,
  },
  {
    path: 'dailyprogressreport',
    component: DailyProgressReportComponent,
    data: roleObject,
  },
  {
    path: 'varietywiselift',
    component: VarietywiseliftComponent,
    data: roleObject,
  },
  {
    path: 'varietywiselift',
    component: VarietywiseliftComponent,
    data: roleObject,
  },
  {
    path: 'farmerdetailscheck',
    component: FarmerdetailscheckComponent,
    data: roleObject,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }