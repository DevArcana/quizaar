import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageQuizesComponent } from './manage-quizes/manage-quizes.component';


const routes: Routes = [
  {
    path: '',
    component: ManageQuizesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManageQuizesRoutingModule { }
