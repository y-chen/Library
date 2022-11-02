import { NgModule } from '@angular/core';

import { BookResolver } from './resolvers/book.resolver';
import { BooksResolver } from './resolvers/books.resolver';
import { EventStoreResolver } from './resolvers/event-store.resolver';
import { ApiService } from './services/api.service';
import { BookService } from './services/book.service';
import { EventStoreService } from './services/event-store.service';

@NgModule({
  declarations: [],
  providers: [
    ApiService,
    EventStoreResolver,
    EventStoreService,
    BookResolver,
    BooksResolver,
    BookService,
  ],
  imports: [],
  exports: [],
})
export class CoreModule {}
