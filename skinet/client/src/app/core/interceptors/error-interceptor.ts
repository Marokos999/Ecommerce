import { HttpErrorResponse, HttpEvent ,HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { SnackbarService } from '../services/snackbar.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const route = inject(Router);
  const snackbar = inject(SnackbarService);

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if(err.status === 400) {
        if(err.error.errors) {
          const modelStateErrors = [];
          for(const key in err.error.errors) {
            if(err.error.errors[key]) {
              modelStateErrors.push(err.error.errors[key]);
            }
          }
          throw modelStateErrors.flat();
        }else {
          snackbar.error(err.error.title || err.error);
        }
        snackbar.error(err.error.title || err.error);
      }
      if(err.status === 401) {
        snackbar.error(err.error.title || err.error);
      }
      if(err.status === 404) {
        route.navigateByUrl('/not-found');
      }
      if(err.status === 500) {
        const navigationsExtras: NavigationExtras = {state: {error: err.error}};
        route.navigateByUrl('/server-error', navigationsExtras);
      }
      
      return throwError(() => err);
    })
  )
};
