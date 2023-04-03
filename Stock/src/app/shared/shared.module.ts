import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NumbersOnlyDirective } from '../custom-directives/numbersonly.directive';


@NgModule({
  declarations: [
    NumbersOnlyDirective,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    NumbersOnlyDirective,
  ]
})
export class SharedModule { }