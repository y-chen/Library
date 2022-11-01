import './app/extensions/rxjs.extension';
import './app/extensions/abstract-control.extension';

import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { APP_CONFIG, Config } from './app/models/config.model';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

const baseLocation = new URL(document.baseURI);
const basePath = baseLocation.pathname;

fetch(`${basePath}assets/config.json`)
  .then((response) => response.json())
  .then((config: Config) => {
    if (!config.production) {
      console.log('Using config', config);
      console.log('JSON format', JSON.stringify(config));
    }

    const providers = [
      { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
      { provide: APP_CONFIG, useValue: config },
    ];

    if (environment.production) {
      enableProdMode();
    }

    platformBrowserDynamic(providers).bootstrapModule(AppModule)
      .catch(err => console.log(err));
  })
