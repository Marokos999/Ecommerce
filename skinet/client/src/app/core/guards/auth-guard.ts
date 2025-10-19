import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { map, of } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
const accService = inject(AccountService);
const router = inject(Router);

if (accService.currentUser()) {
  return of(true);
}else {
  return accService.getAuthState().pipe(
    map(auth => {
      if (auth.isAuthenticated) {
        return true;
      }else {
         router.navigate(['account/login'], { queryParams: { returnUrl: state.url } });
         return false;
      }
    })
  )
}
};
