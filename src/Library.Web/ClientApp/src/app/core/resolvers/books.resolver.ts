import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';

import { Book } from '../../models/book.model';
import { Result } from '../../models/result.model';
import { BookService } from '../services/book.service';

@Injectable()
export class BooksResolver implements Resolve<Result<Book>> {
  constructor(private readonly bookService: BookService) {}

  resolve(): Promise<Result<Book>> {
    return this.bookService.readBooks();
  }
}
