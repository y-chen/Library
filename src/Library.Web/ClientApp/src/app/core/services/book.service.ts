import { Injectable } from '@angular/core';

import { Book, BookParam } from '../../models/book.model';
import { Result } from '../../models/result.model';
import { ApiService } from './api.service';

@Injectable()
export class BookService {
  private readonly prefix: string;

  constructor(private readonly api: ApiService) {
    this.prefix = 'books';
  }

  createBook(book: Book): Promise<Book> {
    return this.api.post(this.prefix, book);
  }

  readBooks(params?: BookParam): Promise<Result<Book>> {
    const convertedParams = this.api.convertParams(params);

    return this.api.get(this.prefix, { params: convertedParams });
  }

  readBookById(id: string): Promise<Book> {
    return this.api.get(`${this.prefix}/${id}`);
  }

  updateBook(id: string, book: Book): Promise<Book> {
    return this.api.put(`${this.prefix}/${id}`, book);
  }
}
