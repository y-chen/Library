import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';

import { FullScreenSpinnerComponent } from './components/full-screen-spinner/full-screen-spinner.component';
import { OverlaySpinnerComponent } from './components/overlay-spinner/overlay-spinner.component';
import { ShellComponent } from './components/shell/shell.component';
import { NavItemComponent } from './components/shell/sidebar/nav-item/nav-item.component';
import { SidebarComponent } from './components/shell/sidebar/sidebar.component';
import { ToolbarComponent } from './components/shell/toolbar/toolbar.component';
import { ValidationErrorsComponent } from './components/validation-errors/validation-errors.component';
import { MaterialModule } from './material/material.module';

@NgModule({
  declarations: [
    FullScreenSpinnerComponent,
    NavItemComponent,
    OverlaySpinnerComponent,
    ShellComponent,
    SidebarComponent,
    ToolbarComponent,
    ValidationErrorsComponent,
  ],
  providers: [],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    MaterialModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MaterialModule,

    FullScreenSpinnerComponent,
    NavItemComponent,
    OverlaySpinnerComponent,
    ShellComponent,
    SidebarComponent,
    ToolbarComponent,
    ValidationErrorsComponent,
  ],
})
export class SharedModule {}
