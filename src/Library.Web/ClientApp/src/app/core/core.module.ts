import { NgModule } from '@angular/core';

import { BooksResolver } from './resolvers/books.resolver';
import { ApiService } from './services/api.service';
import { BookService } from './services/book.service';

@NgModule({
  declarations: [],
  providers: [ApiService, BooksResolver, BookService],
  imports: [],
  exports: [],
})
export class CoreModule {}
