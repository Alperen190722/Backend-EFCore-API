import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http'; 
import { appRoutes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { AlertifyService } from './services/alertify.service';
import { NgxEditorModule } from 'ngx-editor';
import { FileUploadModule } from 'ng2-file-upload';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(),
    provideAnimations(),
    AlertifyService,
    NgxEditorModule,
    FileUploadModule
  ]
};
