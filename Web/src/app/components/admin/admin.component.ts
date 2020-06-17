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
  files: File[] = [];

  constructor(
    public authService: AuthService
  ) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('userData')) as Admin;
  }

  /**
   * Activates when file is dropped in zone
   */
  onSelect(event) {
    this.files.push(...event.addedFiles);
  }

  /**
   * Activates when file is removed from drop zone
   */
  onRemove(event) {
    this.files.splice(this.files.indexOf(event), 1);
  }

  upload() {
    // ANALYZE .XLS FILE
  }

}
