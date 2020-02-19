import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManageQuizesRoutingModule } from './manage-quizes-routing.module';
import { ManageQuizesComponent } from './manage-quizes/manage-quizes.component';


@NgModule({
  declarations: [ManageQuizesComponent],
  imports: [
    CommonModule,
    ManageQuizesRoutingModule
  ]
})
export class ManageQuizesModule { }
