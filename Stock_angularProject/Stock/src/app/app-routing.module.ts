import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../app/login/login.component'
import { HomeComponent } from './home/home/home.component';
import { StocksaleComponent } from './stocksale/stocksale.component';
import { LayoutComponent } from './layout/layout.component';
import { PubliclayoutComponent } from './public/publiclayout/publiclayout.component';
import { AuthguardGuard } from './guards/authguard.guard';

const routes: Routes = [
  // {
  //   path: '',
  //   redirectTo: 'login',
  //   pathMatch: 'full'
  // },
  { path: '', component: LoginComponent },
  { path: 'login', loadChildren: () => import('../app/login/login.module').then(module => module.LoginModule) },
 
  { path: 'home', component: HomeComponent },
  // { path: 'dealersale', component: StocksaleComponent },
  {
    path: 'farmersale',
    component: LayoutComponent,
    loadChildren: () => import('../app/farmer-sale/farmer-sale.module').then(module => module.FarmerSaleModule)
  },
  {
    path: 'dealer',
    component: LayoutComponent,
    loadChildren: () => import('../app/dealer-sale/dealer-sale.module').then(module => module.DealerSaleModule)
  },{
    path: 'aao',
    component: LayoutComponent,
    data: {
      role: 'AAOO'
    },
    canActivateChild: [AuthguardGuard],
    loadChildren: () => import('../app/aao/aao.module').then(module => module.AaoModule)
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
