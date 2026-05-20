import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { isDevMode } from '@angular/core';



if (!isDevMode()) {
  window.console.log = () => {};
  window.console.info = () => {};
  window.console.warn = () => {};

}

bootstrapApplication(App, appConfig)
  .catch((err) => console.error(err));
