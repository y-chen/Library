import { BehaviorSubject, debounceTime, distinctUntilChanged, skip, Subject } from 'rxjs';

import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
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
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  length!: number;
  pageSize: number = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  result!: Result<Book>;
  displayedColumns: string[] = ['title', 'description', 'publishDate', 'author', 'actions'];
  searchTerm: string = '';
  searchTerm$ = new BehaviorSubject<string>('');

  directionMapping = {
    asc: 'ASC',
    desc: 'DESC',
    '': 'ASC',
  };

  constructor(private readonly bookService: BookService, private readonly route: ActivatedRoute) {}

  ngOnInit(): void {
    this.result = this.route.snapshot.data['result'];

    this.searchTerm$.pipe(debounceTime(500), distinctUntilChanged()).subscribe(async (value) => {
      this.searchTerm = value;
      this.result = await this.bookService.readBooks({ searchTerm: this.searchTerm });
    });
  }

  async onPageChange({ previousPageIndex, pageIndex, pageSize: take }: PageEvent) {
    const skip = this.getSkip(take, pageIndex, previousPageIndex);

    this.result = await this.bookService.readBooks({ skip, take });
  }

  async sort({ active: orderBy, direction }: Sort) {
    this.result = await this.bookService.readBooks({
      searchTerm: this.searchTerm,
      orderBy,
      orderDirection: this.directionMapping[direction],
      skip: this.getSkip(this.paginator.pageSize, this.paginator.pageIndex),
      take: this.paginator.pageSize,
    });
  }

  private getSkip(take: number, pageIndex: number, previousPageIndex?: number) {
    return take > this.result.count
      ? 0
      : pageIndex > (previousPageIndex ?? 0)
      ? ((previousPageIndex ?? 0) + 1) * take
      : pageIndex * take;
  }
}
