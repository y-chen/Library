import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

import { ValidationError } from '../../../models/validation-errors.model';

@Component({
	selector: 'lib-validation-errors',
	templateUrl: './validation-errors.component.html',
	styleUrls: ['./validation-errors.component.scss'],
})
export class ValidationErrorsComponent {
	@Input() control!: FormControl;
	@Input() errors!: ValidationError[];
}
