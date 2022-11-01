import { firstValueFrom, Observable, Subscription } from 'rxjs';

declare module 'rxjs' {
  
  interface Observable<T> {
    promisify<T>(): Promise<T>;
  }
}

if (!Observable.prototype.promisify) {
  Observable.prototype.promisify = function <T>(this: Observable<T>): Promise<T> {
    return firstValueFrom(this);
  };
}