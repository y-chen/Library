import { ParamBase } from './param.model';

export interface Book {
  id: string;
  title: string;
  description: string;
  publishDate: Date;
  author: string;
}

export type BookParam = ParamBase;
