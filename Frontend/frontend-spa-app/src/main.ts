import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ConfigService } from './app/services/config.service';

async function loadConfig(): Promise<any> {
  try {
    const response = await fetch('/assets/config.json');
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error('Error loading configuration:', error);
    return {};
  }
}

loadConfig().then((config) => {
  bootstrapApplication(AppComponent, {
    providers: [
      importProvidersFrom(HttpClientModule),
      {
        provide: ConfigService,
        useFactory: () => {
          const service = new ConfigService();
          service.setConfig(config); // Inject config
          return service;
        },
      },
      ...appConfig.providers,
    ],
  }).catch((err) => console.error(err));
});
