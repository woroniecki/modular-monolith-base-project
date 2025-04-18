import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  private config: Config | null = null;

  setConfig(config: Config): void {
    this.config = config;
  }

  get apiUrl(): string {
    return this.config?.apiBaseUrl || 'fallback-url';
  }

  get appName(): string {
    return this.config?.appName || 'fallback-url';
  }
}

interface Config {
  apiBaseUrl: string;
  appName: string;
}
