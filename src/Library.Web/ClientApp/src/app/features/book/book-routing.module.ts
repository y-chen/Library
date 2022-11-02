import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BooksResolver } from '../../core/resolvers/books.resolver';
import { BookListComponent } from './components/book-list/book-list.component';

const routes: Routes = [
  {
    path: '',
    component: BookListComponent,
    resolve: { result: BooksResolver },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BookRoutingModule {}
