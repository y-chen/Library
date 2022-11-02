import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { CoreModule } from '../../core/core.module';
import { SharedModule } from '../../shared/shared.module';
import { BookRoutingModule } from './book-routing.module';
import { BookEditorComponent } from './components/book-editor/book-editor.component';
import { BookListComponent } from './components/book-list/book-list.component';

@NgModule({
  declarations: [BookEditorComponent, BookListComponent],
  providers: [],
  imports: [CoreModule, BookRoutingModule, CommonModule, SharedModule],
  exports: [],
})
export class BookModule {}
