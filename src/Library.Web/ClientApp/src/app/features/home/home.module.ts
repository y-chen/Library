import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared/shared.module';
import { HomepageComponent } from './components/homepage/homepage.component';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
  declarations: [HomepageComponent],
  providers: [],
  imports: [CommonModule, HomeRoutingModule, SharedModule],
  exports: [HomepageComponent],
})
export class HomeModule {}
