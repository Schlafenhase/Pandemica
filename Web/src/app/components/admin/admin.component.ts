import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth/auth.service';
import {Admin, HealthCenter} from '../../services/data/users';
import {FilesService} from '../../services/admin/files.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  user: any;
  files: File[] = [];

  constructor(
    public authService: AuthService,
    public filesService: FilesService
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

  // Sends the files to the api
  upload() {
    this.filesService.SendPatients(this.files)
      .subscribe(response =>{
        console.log(response);
      });
  }

}
