import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class SecureInnerPagesGuard implements CanActivate {

  constructor(
    public authService: AuthService,
    public router: Router
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if(this.authService.isLoggedIn) {
      const role =localStorage.getItem('role');
      switch (role) {
        case 'user':
          this.router.navigate(['user-dashboard'])
          break
        case 'health-center':
          this.router.navigate(['health-center-dashboard'])
          break
        case 'admin':
          this.router.navigate(['admin-dashboard'])
          break
        case 'doctor':
          this.router.navigate(['doctor-dashboard'])
          break;
      }
    }

    return true;
  }

}
