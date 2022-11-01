import { Observable, Subscriber } from 'rxjs';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import JsonURL from '@jsonurl/jsonurl';

import { APP_CONFIG, Config } from '../../models/config.model';
import { Header, HttpOption, HttpOptions, Param } from '../../models/http.model';

@Injectable()
export class ApiService {
  private readonly apiRoot: string;

  constructor(@Inject(APP_CONFIG) private readonly config: Config, private readonly http: HttpClient) {
    this.apiRoot = this.config.apiBaseUrl;
  }

  get<T>(relativeUrl: string, options?: HttpOptions): Promise<T> {
    const url = this.buildUrl(relativeUrl);
    const opts = this.getOptions(options);

    return this.http.get(url, opts).promisify();
  }

  getBlob(relativeUrl: string, options?: HttpOptions): Observable<string> {
    const url = this.buildUrl(relativeUrl);
    const opts = this.getOptions(options);

    return new Observable((observer: Subscriber<string>) => {
      let objectUrl: string | null = null;

      this.http.get(url, { ...opts, responseType: 'blob' }).subscribe((data) => {
        objectUrl = URL.createObjectURL(data);
        observer.next(objectUrl);
      });

      return () => {
        if (objectUrl) {
          URL.revokeObjectURL(objectUrl);
          objectUrl = null;
        }
      };
    });
  }

  post<T>(relativeUrl: string, body?: unknown, options?: HttpOptions): Promise<T> {
    const url = this.buildUrl(relativeUrl);
    const opts = this.getOptions(options);

    if (body instanceof FormData) {
      opts.headers = opts.headers.delete('Content-Type');
    }

    return this.http.post<T>(url, body, opts).promisify();
  }

  put<T>(relativeUrl: string, body?: unknown, options?: HttpOptions): Promise<T> {
    const url = this.buildUrl(relativeUrl);
    const opts = this.getOptions(options);

    return this.http.put<T>(url, body, opts).promisify();
  }

  delete<T>(relativeUrl: string, options?: HttpOptions): Promise<T> {
    const url = this.buildUrl(relativeUrl);
    const opts = this.getOptions(options);

    return this.http.delete<T>(url, opts).promisify();
  }

  patch<T>(relativeUrl: string, body?: unknown): Promise<T> {
    const url = this.buildUrl(relativeUrl);
    const opts = { headers: new HttpHeaders() };
    opts.headers = this.getHeaders([]);

    return this.http.patch<T>(url, body, opts).promisify();
  }

  convertParams(params: object = {}): Param[] {
    return Object.entries(params).map(([key, value]) => ({ key, value }));
  }

  private buildUrl = (relativeUrl: string): string => `${this.apiRoot}/${relativeUrl}`;

  private getOptions(options?: HttpOptions): { headers: HttpHeaders; params: HttpParams } {
    const { headers, params } = options || { headers: [], params: [] };

    return {
      headers: this.getHeaders(headers),
      params: this.getParams(params),
    };
  }

  private getHeaders(customHeaders?: Header[]): HttpHeaders {
    let headers = new HttpHeaders();
    customHeaders = customHeaders || [];
    headers = customHeaders.reduce(
      (accumulator: HttpOption, currentValue: Header) => (headers = this.handleOption(accumulator, currentValue) as HttpHeaders),
      headers,
    );
    headers = headers.set('Cache-Control', 'no-cache').set('Pragma', 'no-cache').set('Expires', 'Sat, 01 Jan 2000 00:00:00 GMT');

    const hasContentType = headers.has('Content-Type');

    if (!hasContentType) {
      headers = headers.set('Content-Type', 'application/json');
    }

    return headers;
  }

  private getParams(customParams: Param[] = []): HttpParams {
    let params = new HttpParams();
    params = customParams.reduce(
      (accumulator: HttpOption, currentValue: Header) => (accumulator = this.handleOption(accumulator, currentValue) as HttpParams),
      params,
    );

    return params;
  }

  private handleOption(options: HttpOption, { key, value }: Header | Param): HttpOption {
    if (this.isSimpleType(value)) {
      return options.append(key, value);
    }

    if (Array.isArray(value)) {
      value.forEach((value) => {
        const parsedValue = typeof value === 'string' ? value : JsonURL.stringify(value);

        options = options.append(key, parsedValue as string);
      });
    }

    return options;
  }

  private isSimpleType(value: unknown): boolean {
    switch (typeof value) {
      case 'number':
      case 'string':
      case 'boolean':
        return true;
    }

    return value instanceof Date;
  }
}
