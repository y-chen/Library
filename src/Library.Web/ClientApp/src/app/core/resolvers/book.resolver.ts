import { isUUID } from 'class-validator';

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';

import { Book } from '../../models/book.model';
import { BookService } from '../services/book.service';

@Injectable()
export class BookResolver implements Resolve<Promise<Book> | undefined> {
  constructor(private readonly bookService: BookService) {}

  resolve(route: ActivatedRouteSnapshot): Promise<Book> | undefined {
    const id: string = route.params['id'];

    if (!isUUID(id)) {
      return;
    }

    return this.bookService.readBookById(id);
  }
}
