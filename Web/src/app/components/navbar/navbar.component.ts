import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  isLoggedIn = false;
  role: any;

  constructor(
    public authService: AuthService,
    public router: Router,
  ) { }

  ngOnInit(): void {
    this.role = localStorage.getItem('role')
    if (this.role != null) {
      this.isLoggedIn = true;
    }
  }

  redirectUser() {
    if (!this.authService.isLoggedIn) {
      // User is not logged in. Go to user access page
      this.router.navigate(['user-access'])
    } else {
      if (localStorage.getItem('role') === 'admin') {
        this.router.navigate(['admin'])
      } else {
        this.router.navigate(['health-center'])
      }
    }
  }

}
