import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-user-access',
  templateUrl: './user-access.component.html',
  styleUrls: ['./user-access.component.scss']
})
export class UserAccessComponent implements OnInit {

  constructor(
    public authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  signIn(email, password, role) {
    this.authService.SignIn(email, password, role);
    this.authService.SignIn(email, password, role);
  }
}



