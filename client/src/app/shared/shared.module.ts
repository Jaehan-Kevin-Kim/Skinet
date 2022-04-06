import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';



@NgModule({
  declarations: [
    PagingHeaderComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    FontAwesomeModule
  ],
  exports: [
    PaginationModule,
    FontAwesomeModule,
    PagingHeaderComponent
  ]
})
export class SharedModule { }
