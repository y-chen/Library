import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';

import { OverlaySpinnerComponent } from './components/overlay-spinner/overlay-spinner.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { ValidationErrorsComponent } from './components/validation-errors/validation-errors.component';
import { MaterialModule } from './material/material.module';

@NgModule({
  declarations: [OverlaySpinnerComponent, ToolbarComponent, ValidationErrorsComponent],
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

    OverlaySpinnerComponent,
    ToolbarComponent,
    ValidationErrorsComponent,
  ],
})
export class SharedModule {}
