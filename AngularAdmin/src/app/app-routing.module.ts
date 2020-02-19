import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadChildren: () => import('./routes/home/home.module').then(m => m.HomeModule)
  },
  {
    path: 'account',
    loadChildren: () => import('./routes/account/account.module').then(m => m.AccountModule)
  },
  {
    path: 'manage-questions',
    loadChildren: () => import('./routes/manage-questions/manage-questions.module').then(m => m.ManageQuestionsModule)
  },
  {
    path: 'manage-quizes',
    loadChildren: () => import('./routes/manage-quizes/manage-quizes.module').then(m => m.ManageQuizesModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
