import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageQuestionsComponent } from './manage-questions/manage-questions.component';


const routes: Routes = [
  {
    path: '',
    component: ManageQuestionsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManageQuestionsRoutingModule { }
