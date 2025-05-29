import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductosRoutingModule } from './productos-routing.module';
import { NgxPaginationModule } from 'ngx-pagination'
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ProductosRoutingModule,
    NgxPaginationModule, 
    FormsModule,
    ReactiveFormsModule
  ]
})
export class ProductosModule { }
