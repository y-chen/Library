import { Injectable } from '@angular/core';

import { Book, BookParam } from '../../models/book.model';
import { EventStore, EventStoreParam } from '../../models/event-store.model';
import { Result } from '../../models/result.model';
import { ApiService } from './api.service';

@Injectable()
export class EventStoreService {
  private readonly prefix: string;

  constructor(private readonly api: ApiService) {
    this.prefix = 'event-store';
  }

  readEvents(params?: EventStoreParam): Promise<Result<EventStore>> {
    const convertedParams = this.api.convertParams(params);

    return this.api.get(this.prefix, { params: convertedParams });
  }
}
