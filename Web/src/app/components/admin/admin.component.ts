import {Component, HostListener, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth/auth.service';
import {Admin, HealthCenter} from '../../services/data/users';
import {FilesService} from '../../services/admin/files.service';
import {environment} from '../../../environments/environment';
import axios from 'axios'

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  user: any;
  files: File[] = [];
  public currentWindowWidth: number;

  constructor(
    public authService: AuthService,
    public filesService: FilesService
  ) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('userData')) as Admin;

    axios.post(environment.serverURL + 'Country/Email', {
      name: 'null',
      continentName: 'null',
      eMail: this.user.email
    }, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.user.country = response.data[0].name;
        this.user.continent = response.data[0].continentName;
      })
      .catch(error => {
        console.log(error.response);
      });

    // Set initial window width
    this.currentWindowWidth = window.innerWidth;
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

  /**
   * Listen for real time window resizing
   */
  @HostListener('window:resize')
  onResize() {
    this.currentWindowWidth = window.innerWidth
  }

}
