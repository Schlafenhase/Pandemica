import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-worker-access',
  templateUrl: './worker-access.component.html',
  styleUrls: ['./worker-access.component.scss']
})
export class WorkerAccessComponent implements OnInit {

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
