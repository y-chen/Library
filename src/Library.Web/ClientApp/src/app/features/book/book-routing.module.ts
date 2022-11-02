import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BooksResolver } from '../../core/resolvers/books.resolver';
import { BookEditorComponent } from './components/book-editor/book-editor.component';
import { BookListComponent } from './components/book-list/book-list.component';

const routes: Routes = [
  {
    path: '',
    component: BookListComponent,
    resolve: { result: BooksResolver },
  },
  {
    path: ':id',
    component: BookEditorComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BookRoutingModule {}
