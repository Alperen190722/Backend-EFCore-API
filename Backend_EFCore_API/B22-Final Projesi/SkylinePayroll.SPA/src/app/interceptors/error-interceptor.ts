
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { NotifService } from '../services/notif.service'; 
import { Result } from '../models/result/result';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private notif: NotifService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap({
        next: (event) => {
          if (event instanceof HttpResponse) {

            const body = event.body as Result;


            if (body && body.success === false) {
              this.notif.error(body.message || 'An unexpected error occurred.');
            }
          }
        },
        error: (err) => {

          if (err.status === 401) {
            this.notif.error('Unauthorized: Please login again.');
          } else if (err.status === 403) {
            this.notif.error('Forbidden: You do not have permission for this action.');
          } else {
            this.notif.error('System Error: Please contact IT Department.');
          }
        }
      })
    );
  }
}
