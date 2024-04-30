import {TuiRootModule} from '@taiga-ui/core';
import {importProvidersFrom} from '@angular/core';
import { bootstrapApplication, provideClientHydration } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';
import {routes} from './app/app.routes';
 
bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),
    provideRouter(routes),
    provideClientHydration(),
    importProvidersFrom(
      TuiRootModule,
      // ...
    ),
  ],
}).catch(err => console.error(err));