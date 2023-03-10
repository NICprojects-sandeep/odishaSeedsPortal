import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../app/login/login.component'
import { HomeComponent } from './home/home/home.component';
import { StocksaleComponent } from './stocksale/stocksale.component';
import { FarmersaleComponent } from './farmersale/farmersale.component';
import { LayoutComponent } from './layout/layout.component';
import { TransferOfSeedSubsidyForGroundnutComponent } from './public/transfer-of-seed-subsidy-for-groundnut/transfer-of-seed-subsidy-for-groundnut.component';
import { PubliclayoutComponent } from './public/publiclayout/publiclayout.component';

const routes: Routes = [
  // {
  //   path: '',
  //   redirectTo: 'login',
  //   pathMatch: 'full'
  // },
  { path: '', component: LoginComponent },
  { path: 'login', loadChildren: () => import('../app/login/login.module').then(module => module.LoginModule) },
  {
    path: 'seedDBT',
    component: LayoutComponent,
    loadChildren: () => import('../app/seed-dbt/seed-dbt.module').then(module => module.SeedDBTModule)
  },
  { path: 'home', component: HomeComponent },
  { path: 'dealersale', component: StocksaleComponent },
  {
    path: 'farmersale',
    component: LayoutComponent,
    loadChildren: () => import('../app/farmer-sale/farmer-sale.module').then(module => module.FarmerSaleModule)
  },

  // {path:'ransferOfSeedSubsidy', component: TransferOfSeedSubsidyForGroundnutComponent},
  {path:'public',   component: PubliclayoutComponent,   loadChildren: () => import('../app/public/public.module').then(module => module.PublicModule)}

];

@NgModule({
  imports: [RouterModule.forRoot(routes,{ useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [HomeComponent]
