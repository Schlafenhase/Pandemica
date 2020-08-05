import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { FormBuilder } from '@angular/forms';
import axios from 'axios';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-user-access',
  templateUrl: './user-access.component.html',
  styleUrls: ['./user-access.component.scss']
})
export class UserAccessComponent implements OnInit {
  signInForm;
  signUpForm;
  sexes: string[] = ['M', 'F'];
  regions: string[];
  countries: string[];
  hospitals: string[];
  region: '';
  nationality: '';
  sex: '';
  hospital: '';
  birthDate: string;
  startDate = new Date();

  constructor(
    public authService: AuthService,
    private formBuilder: FormBuilder,
  ) {
    // Initialize Sign In Form
    this.signInForm = this.formBuilder.group({
      userEmail: '',
      userPassword: ''
    });

    // Initialize Sign Up Form
    this.signUpForm = this.formBuilder.group({
      newEmail: '',
      newPassword: '',
      newName: '',
      newLastName: '',
      newSSN: '',
      newRegion: '',
      newNationality: '',
      newHospital: '',
      newSex: ''
    });
  }

  ngOnInit(): void {
    this.birthDate = '';
    this.getCountries();
  }

  getCountries() {
    axios.get(environment.serverURL + 'Country/Names', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.countries = response.data;
        this.getRegions();
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  getRegions() {
    axios.get(environment.secondWaveURL + 'Region/CostaRica', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.regions = response.data;
        this.getHospitals();
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  getHospitals() {
    axios.get(environment.secondWaveURL + 'Hospital/CostaRica', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.hospitals = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Set region
   * @param event selected region
   */
  selectedRegion(event) {
    this.region = event.value;
  }

  /**
   * Set nationality
   * @param event selected nationality
   */
  selectedNationality(event) {
    this.nationality = event.value;
  }

  selectedHospital(event) {
    this.hospital = event.value;
  }

  selectedSex(event) {
    this.sex = event.value;
  }

  /*
  Register user using auth service
   */
  signUp(email, password, name, lastName, ssn) {
    // tslint:disable-next-line:max-line-length
    if (email !== '' && password !== '' && name !== '' && lastName !== '' && ssn !== '' && this.birthDate !== '' && this.region !== '' && this.nationality !== '' && this.hospital !== '' && this.sex !== '' && password.length > 5){
      axios.post(environment.secondWaveURL + 'Patient', {
        Ssn: ssn,
        FirstName: name,
        LastName: lastName,
        BirthDate: this.birthDate,
        Hospitalized: false,
        Icu: false,
        Country: 'Costa Rica',
        Region: this.region,
        Nationality: this.nationality,
        Hospital: this.hospital,
        Sex: this.sex,
        Email: email
      }, {
        headers: {
          'Content-Type': 'application/json; charset=UTF-8'
        }
      })
        .then(response => {
          console.log(response);
          this.authService.SignUp(email, password, 'user');
        })
        .catch(error => {
          console.log(error.response);
        });
    }
  }

  /*
  Signs in user using auth service
    */
  signIn(email, password) {
    const role = 'user';
    this.authService.SignIn(email, password, role);
    this.authService.SignIn(email, password, role);
    // if (email !== '' && password !== ''){
    //   axios.post(environment.secondWaveURL + 'Patient/Email', {
    //     Email: email
    //   }, {
    //     headers: {
    //       'Content-Type': 'application/json; charset=UTF-8'
    //     }
    //   })
    //     .then(response => {
    //       console.log(response);
    //       if (response.data !== null){
    //         this.authService.SignIn(email, password, role);
    //         this.authService.SignIn(email, password, role);
    //       }
    //     })
    //     .catch(error => {
    //       console.log(error.response);
    //     });
    // }
  }

  /**
   * Set birth date
   * @param dateObject selected date
   */
  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.birthDate = stringified.substring(1, 11);
  }
}



