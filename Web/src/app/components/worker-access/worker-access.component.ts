import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import axios from 'axios';
import {environment} from '../../../environments/environment';

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
    if (role === 'doctor'){
      axios.post(environment.secondWaveURL + 'HealthWorker/Email', {
        Email: email
      }, {
        headers: {
          'Content-Type': 'application/json; charset=UTF-8'
        }
      })
        .then(response => {
          console.log(response);
          if (response.data !== null){
            this.authService.SignIn(email, password, role);
            this.authService.SignIn(email, password, role);
          }
        })
        .catch(error => {
          console.log(error.response);
        });
    }else {
      this.authService.SignIn(email, password, role);
      this.authService.SignIn(email, password, role);
    }
  }

}
