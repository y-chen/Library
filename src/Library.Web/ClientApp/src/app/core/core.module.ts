import { NgModule } from '@angular/core';

import { BookResolver } from './resolvers/book.resolver';
import { BooksResolver } from './resolvers/books.resolver';
import { ApiService } from './services/api.service';
import { BookService } from './services/book.service';

@NgModule({
  declarations: [],
  providers: [ApiService, BookResolver, BooksResolver, BookService],
  imports: [],
  exports: [],
})
export class CoreModule {}
