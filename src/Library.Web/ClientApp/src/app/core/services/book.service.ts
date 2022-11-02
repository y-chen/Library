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

  readBooks(params?: BookParam): Promise<Result<Book>> {
    const convertedParams = this.api.convertParams(params);

    return this.api.get(this.prefix, { params: convertedParams });
  }
}
