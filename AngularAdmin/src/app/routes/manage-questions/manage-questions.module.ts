import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManageQuestionsRoutingModule } from './manage-questions-routing.module';
import { ManageQuestionsComponent } from './manage-questions/manage-questions.component';


@NgModule({
  declarations: [ManageQuestionsComponent],
  imports: [
    CommonModule,
    ManageQuestionsRoutingModule
  ]
})
export class ManageQuestionsModule { }
