import { ApplicationConfig, inject, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import {
  provideClientHydration,
  withEventReplay,
} from '@angular/platform-browser';
import {
  provideHttpClient,
  withFetch,
  withInterceptors,
} from '@angular/common/http';
import { credentialsInterceptor } from './credential.interceptor';
import { AuthService } from './services/auth.service';
import { ConfigService } from './services/config.service';
import { ApiConfiguration } from './api-client/api-configuration';
import { provideAngularSvgIcon } from 'angular-svg-icon';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideClientHydration(withEventReplay()),
    provideHttpClient(withFetch(), withInterceptors([credentialsInterceptor])),
    provideAngularSvgIcon(),
    provideAppInitializer(() => {
      const initAuth = ((authService: AuthService) => {
        return () => {
          return authService.tryToLoginWithRefreshToken();
        };
      })(inject(AuthService));
      return initAuth();
    }),
    provideAppInitializer(() => {
      const initBaseUrlSetting = ((config: ConfigService, apiConfig: ApiConfiguration) => {
        return () => {
          apiConfig.rootUrl = config.apiUrl;
          return Promise.resolve();
        };
      })(inject(ConfigService), inject(ApiConfiguration));
      return initBaseUrlSetting();
    })],
};
