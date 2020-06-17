import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth/auth.service';
import {Admin, HealthCenter} from '../../services/data/users';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  user: any;

  constructor(
    public authService: AuthService
  ) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('userData')) as Admin;
  }

}
