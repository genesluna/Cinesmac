import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        if (err) {
          switch (err.status) {
            case 400:
              if (err.error.erros) throw err.error;
              else this.toastr.error(err.error.message, err.error.statusCode);
              break;
            case 401:
              this.toastr.error(err.error.message, err.error.statusCode);
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              const navExtras: NavigationExtras = {
                state: { error: err.error },
              };
              this.router.navigateByUrl('/server-error', navExtras);
              break;

            default:
              break;
          }
        }
        return throwError(() => new Error(err.message));
      })
    );
  }
}
