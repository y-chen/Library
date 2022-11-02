import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';

import { EventStoreService } from '../../../../core/services/event-store.service';
import { ComparisonEntry } from '../../../../models/comparison-entry.model';
import { EventStore } from '../../../../models/event-store.model';
import { Result } from '../../../../models/result.model';

@Component({
  selector: 'lib-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss'],
})
export class HistoryComponent implements OnInit {
  result!: Result<EventStore>;

  first!: any;
  second!: any;

  comparison!: ComparisonEntry[];

  displayedColumns: string[] = ['propertyName', 'before', 'after'];

  constructor(
    private readonly eventStore: EventStoreService,
    private readonly route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.result = this.route.snapshot.data['result'];
    this.refreshComparison();
  }

  async onPageChange({ previousPageIndex, pageIndex, pageSize: take }: PageEvent) {
    const skip =
      take > this.result.count
        ? 0
        : pageIndex > (previousPageIndex ?? 0)
        ? ((previousPageIndex ?? 0) + 1) * take
        : pageIndex * take;

    this.result = await this.eventStore.readEvents({
      streamId: this.first.streamId,
      streamName: this.first.streamName,
      skip: skip - 1,
      take,
    });

    this.refreshComparison();
  }

  private refreshComparison() {
    [this.first, this.second] = this.result.items.map(
      ({ streamId, streamName, eventType, data, revision }) => {
        return {
          streamId,
          streamName,
          eventType,
          revision,
          ...data,
        };
      },
    );

    this.comparison = Object.entries(this.first).reduce((accumulator: ComparisonEntry[], [key]) => {
      const before = this.first[key];
      const after = this.second[key];

      const entry: ComparisonEntry = {
        propertyName: key,
        before,
        after,
        same: before === after,
      };

      accumulator.push(entry);

      return accumulator;
    }, []);
  }
}
