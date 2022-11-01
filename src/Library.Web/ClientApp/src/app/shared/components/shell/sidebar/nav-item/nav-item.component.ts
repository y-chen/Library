import { Component, Input } from '@angular/core';

import { NavItem } from '../../../../../models/navigation.model';

@Component({
	selector: 'lib-nav-item',
	templateUrl: './nav-item.component.html',
	styleUrls: ['./nav-item.component.scss'],
})
export class NavItemComponent {
	@Input() item!: NavItem;
	@Input() open!: boolean;
}
