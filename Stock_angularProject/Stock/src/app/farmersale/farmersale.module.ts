import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FarmersaleComponent } from './farmersale.component';
import { HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    // FarmersaleComponent
  ],
  imports: [
    CommonModule,
    HttpClient,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class FarmersaleModule { }
