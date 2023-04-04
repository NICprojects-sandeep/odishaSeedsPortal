import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'transaction',
    loadChildren: () => import('./transaction/transaction.module').then(module => module.TransactionModule)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SeedDBTRoutingModule { }
