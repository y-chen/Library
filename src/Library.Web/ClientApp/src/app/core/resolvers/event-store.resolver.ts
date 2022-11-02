import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';

import { EventStore } from '../../models/event-store.model';
import { Result } from '../../models/result.model';
import { EventStoreService } from '../services/event-store.service';

@Injectable()
export class EventStoreResolver implements Resolve<Result<EventStore>> {
  constructor(private readonly eventStoreService: EventStoreService) {}

  resolve(route: ActivatedRouteSnapshot): Promise<Result<EventStore>> {
    const { streamId, streamName, orderBy, orderDirection, skip, take } = route.queryParams;

    return this.eventStoreService.readEvents({
      streamId,
      streamName,
      orderBy,
      orderDirection,
      skip,
      take,
    });
  }
}
