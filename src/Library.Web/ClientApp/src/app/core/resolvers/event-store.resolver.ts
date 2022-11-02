import { Injectable } from '@angular/core';
import { ActivatedRoute, Resolve } from '@angular/router';

import { Book } from '../../models/book.model';
import { EventStore } from '../../models/event-store.model';
import { Result } from '../../models/result.model';
import { BookService } from '../services/book.service';
import { EventStoreService } from '../services/event-store.service';

@Injectable()
export class EventStoreResolver implements Resolve<Result<EventStore>> {
  constructor(
    private readonly eventStoreService: EventStoreService,
    private route: ActivatedRoute,
  ) {}

  resolve(): Promise<Result<EventStore>> {
    const { streamId, stringName, orderBy, orderDirection, skip, take } =
      this.route.snapshot.queryParams;

    return this.eventStoreService.readEvents({
      streamId,
      stringName,
      orderBy,
      orderDirection,
      skip,
      take,
    });
  }
}
