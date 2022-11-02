import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared/shared.module';
import { HistoryComponent } from './components/homepage/history.component';
import { HistoryRoutingModule } from './history-routing.module';

@NgModule({
  declarations: [HistoryComponent],
  providers: [],
  imports: [CommonModule, HistoryRoutingModule, SharedModule],
  exports: [],
})
export class HistoryModule {}
