import { isUUID } from 'class-validator';

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BookService } from '../../../../core/services/book.service';
import { Book } from '../../../../models/book.model';
import { ValidationErrors } from '../../../../models/validation-errors.model';

@Component({
  selector: 'lib-book-editor',
  templateUrl: './book-editor.component.html',
  styleUrls: ['./book-editor.component.scss'],
})
export class BookEditorComponent implements OnInit {
  book?: Book;
  form!: FormGroup;

  protected readonly validationErrors: ValidationErrors = {
    title: [{ type: 'required', message: 'Title is required.' }],
    description: [{ type: 'required', message: 'Description is required.' }],
    publishDate: [{ type: 'required', message: 'Publish is required.' }],
    author: [{ type: 'required', message: 'Author is required.' }],
  };

  constructor(
    private readonly bookService: BookService,
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
  ) {}

  ngOnInit(): void {
    this.book = this.route.snapshot.data['book'];
    this.form = this.createForm(this.book);
  }

  async submit(): Promise<void> {
    const values = this.form.value;

    isUUID(values.id)
      ? await this.bookService.updateBook(values.id, values)
      : await this.bookService.createBook({ ...values, id: undefined });

    this.router.navigate(['/app', 'books']);
  }

  private createForm(book?: Book): FormGroup {
    const { id, title, description, publishDate, author } = book ?? {};

    return this.formBuilder.group({
      id: [id ?? undefined, []],
      title: [title, [Validators.required]],
      description: [description, [Validators.required]],
      publishDate: [publishDate, [Validators.required]],
      author: [author, [Validators.required]],
    });
  }
}
