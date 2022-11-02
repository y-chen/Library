import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';

import { BookService } from '../../../../core/services/book.service';
import { Book } from '../../../../models/book.model';
import { Result } from '../../../../models/result.model';

@Component({
  selector: 'lib-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss'],
})
export class BookListComponent implements OnInit {
  length!: number;
  pageSize: number = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  result!: Result<Book>;
  displayedColumns: string[] = ['title', 'description', 'publishDate', 'authors', 'actions'];

  constructor(private readonly bookService: BookService, private readonly route: ActivatedRoute) {}

  ngOnInit(): void {
    this.result = this.route.snapshot.data['result'];
  }

  async onPageChange({ previousPageIndex, pageIndex, pageSize: take }: PageEvent) {
    const skip =
      take > this.result.count
        ? 0
        : pageIndex > (previousPageIndex ?? 0)
        ? ((previousPageIndex ?? 0) + 1) * take
        : pageIndex * take;

    this.result = await this.bookService.readBooks({ skip, take });
  }
}
