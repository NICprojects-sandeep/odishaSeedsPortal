import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../app/login/login.component'
import { HomeComponent } from './home/home/home.component';
import { StocksaleComponent } from './stocksale/stocksale.component';
import { LayoutComponent } from './layout/layout.component';
import { PubliclayoutComponent } from './public/publiclayout/publiclayout.component';
import { AuthguardGuard } from './guards/authguard.guard';
import { ForgotpasswordComponent } from './login/forgotpassword/forgotpassword.component';

const routes: Routes = [
  // {
  //   path: '',
  //   redirectTo: 'login',
  //   pathMatch: 'full'
  // },
  { path: '', component: LoginComponent },
  { path: 'login', loadChildren: () => import('../app/login/login.module').then(module => module.LoginModule) },
  { path: 'fgpwd', component:ForgotpasswordComponent},
  { path: 'home', component: HomeComponent },
  // { path: 'dealersale', component: StocksaleComponent },
  {
    path: 'farmersale',
    component: LayoutComponent,
    data: {
      role: 'Dealer'
    },
    canActivateChild: [AuthguardGuard],
    loadChildren: () => import('../app/farmer-sale/farmer-sale.module').then(module => module.FarmerSaleModule)
  },
  {
    path: 'dealer',
    component: LayoutComponent,
    data: {
      role: 'SPO'
    },
    canActivateChild: [AuthguardGuard],
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
  {
    path: 'admin',
    component: LayoutComponent,
    data: {
      role: 'ADMI'
    },
    canActivateChild: [AuthguardGuard],
    loadChildren: () => import('../app/admin/admin.module').then(module => module.AdminModule)
  },
  {
    path: 'dda',
    component: LayoutComponent,
    data: {
      role: 'DIST'
    },
    canActivateChild: [AuthguardGuard],
    loadChildren: () => import('../app/dda/dda.module').then(module => module.DdaModule)
  },
  {
    path: 'jrcs',
    component: LayoutComponent,
    data: {
      role: 'jrcs'
    },
    canActivateChild: [AuthguardGuard],
    loadChildren: () => import('../app/jrcs/jrcs.module').then(module => module.JrcsModule)
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
