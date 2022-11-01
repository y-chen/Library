import { InjectionToken } from '@angular/core';

export interface Config {
  production: boolean;
  apiBaseUrl: string;
}

export const APP_CONFIG = new InjectionToken<Config>('config');
