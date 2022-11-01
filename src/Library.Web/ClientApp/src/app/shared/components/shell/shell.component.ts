import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NavItem } from '../../../models/navigation.model';

@Component({
	selector: 'lib-shell',
	templateUrl: './shell.component.html',
	styleUrls: ['./shell.component.scss'],
})
export class ShellComponent implements OnInit {
	navItems!: NavItem[];

	constructor(private router: Router, private readonly route: ActivatedRoute) {}

	ngOnInit(): void {
		this.route.data.subscribe((data) => {
			this.navItems = data.navItems;
		});

		this.router.navigate(['/book']);
	}
}
