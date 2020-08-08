import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';
import {AuthService} from '../../../../services/auth/auth.service';
import Swal from "sweetalert2";

@Component({
  selector: 'app-contacts-upgrade',
  templateUrl: './contacts-upgrade.component.html',
  styleUrls: ['./contacts-upgrade.component.scss']
})

export class ContactsUpgradeComponent implements OnInit {
  public _elementForm: FormGroup;
  countries: string[];
  item: any;
  regions: string[];
  region: '';
  nationality: '';

  constructor(private _formBuilder: FormBuilder,
              public authService: AuthService,
              private dialogRef: MatDialogRef<ContactsUpgradeComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    this.item = this.data.item;
    console.log(this.item);

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        password: [this.item.password, [Validators.required]],
      });
      this.getCountries();
    }
  }

  /**
   * Get countries from the database
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'upgrade-contact'});
  }

  /**
   * Deletes element from database
   */
  deleteElement() {
    axios.delete(environment.serverURL + 'Contact/' + this.item.ssn + '/' + localStorage.getItem('patientSsn'), {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.closeDialogRefresh();
        this.fireSuccessAlert();
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('password') as HTMLInputElement).value = '';
  }

  /**
   * Fire error alert
   */
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'error',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Fire success alert
   */
  fireSuccessAlert(){
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'Everything went smoothly',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Get countries from the database
   */
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
        this.fireErrorAlert();
      });
  }

  /**
   * Get regions from the database
   */
  getRegions() {
    axios.get(environment.serverURL + 'ProvinceStateRegion/Names', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.regions = response.data;
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
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

  /**
   * Updates changes in server
   */
  submit() {
    const pPassword = (document.getElementById('password') as HTMLInputElement).value;

    if (pPassword !== '' && this.region !== '' && this.nationality !== ''){
      axios.post(environment.secondWaveURL + 'Patient/Id', {
        Ssn: this.item.ssn,
        FirstName: this.item.firstName,
        LastName: this.item.lastName,
        BirthDate: this.item.birthDate,
        Hospitalized: false,
        Icu: false,
        Country: 'Costa Rica',
        Region: this.region,
        Nationality: this.nationality,
        Hospital: localStorage.getItem('hospitalId'),
        Sex: this.item.sex,
        Email: this.item.eMail
      }, {
        headers: {
          'Content-Type': 'application/json; charset=UTF-8'
        }
      })
        .then(response => {
          console.log(response);
          this.authService.SignUp(this.item.eMail, pPassword, 'user');
          this.deleteElement();
        })
        .catch(error => {
          console.log(error.response);
          this.fireErrorAlert();
        });
    }
  }

}
