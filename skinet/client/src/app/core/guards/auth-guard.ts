import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
const accService = inject(AccountService);
const router = inject(Router);

if (accService.currentUser()) {
  return true;
}else {
  router.navigate(['account/login'], { queryParams: { returnUrl: state.url } });
  return false;
}
};
