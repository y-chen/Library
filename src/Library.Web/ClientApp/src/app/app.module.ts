import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { BookModule } from './features/book/book.module';
import { HomeModule } from './features/home/home.module';
import { InterceptorsModule } from './interceptors/interceptors.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    AppRoutingModule,
    BookModule,
    BrowserAnimationsModule,
    BrowserModule,
    CoreModule,
    HomeModule,
    InterceptorsModule,
    SharedModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
