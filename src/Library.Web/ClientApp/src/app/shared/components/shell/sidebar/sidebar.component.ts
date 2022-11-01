import { Component, Input } from '@angular/core';

import { NavItem } from '../../../../models/navigation.model';

@Component({
	selector: 'lib-sidebar',
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent {
	@Input() navItems!: NavItem[];

	closed = false;

	toggle(): void {
		this.closed = !this.closed;
	}
}
