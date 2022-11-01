import { Component } from '@angular/core';

import { LoaderService } from '../../../interceptors/services/loader.service';

@Component({
	selector: 'lib-overlay-spinner',
	templateUrl: './overlay-spinner.component.html',
	styleUrls: ['./overlay-spinner.component.scss'],
})
export class OverlaySpinnerComponent {
	constructor(readonly loaderService: LoaderService) { }
}
