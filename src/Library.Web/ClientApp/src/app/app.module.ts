import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InterceptorsModule } from './interceptors/interceptors.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [AppComponent],
  imports: [AppRoutingModule, BrowserModule, InterceptorsModule, SharedModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
