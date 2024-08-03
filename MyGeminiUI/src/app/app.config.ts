import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { provideClientHydration, withHttpTransferCacheOptions } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { CLIPBOARD_OPTIONS, ClipboardButtonComponent, MARKED_OPTIONS, MarkedRenderer, provideMarkdown } from 'ngx-markdown';
import { MessageService } from 'primeng/api';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(withHttpTransferCacheOptions({ includePostRequests: true })),
    provideHttpClient(withFetch(), withInterceptors([])),
    provideAnimations(),
    provideMarkdown({
      clipboardOptions: {
        provide: CLIPBOARD_OPTIONS,
        useValue: {
          buttonComponent: ClipboardButtonComponent,
        },
      },
      markedOptions: {
        provide: MARKED_OPTIONS,
        useValue: {
          renderer: new MarkedRenderer(),
          gfm: true,
          breaks: false,
          pedantic: false,
          smartLists: true,
          lineNumber: true,
        },
      }
    }),
    MessageService
  ]
};
