import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EventStoreResolver } from '../../core/resolvers/event-store.resolver';
import { HistoryComponent } from './components/homepage/history.component';

const routes: Routes = [
  {
    path: '',
    component: HistoryComponent,
    resolve: { result: EventStoreResolver },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HistoryRoutingModule {}
