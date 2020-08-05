import {Component, Inject, OnInit} from '@angular/core';
import {NetworkService} from '../../../services/network/network.service';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import axios from 'axios';
import {environment} from '../../../../environments/environment';
import { formatDate } from '@angular/common';
import {AuthService} from '../../../services/auth/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-health-center-popup',
  templateUrl: './health-center-popup.component.html',
  styleUrls: ['./health-center-popup.component.scss']
})
export class HealthCenterPopupComponent implements OnInit {
  countries: string[];
  regions: string[];
  sexes: string[] = ['M', 'F'];
  public _elementForm: FormGroup;
  type: string;
  item: any;
  country: '';
  region: '';
  nationality: '';
  sex: '';
  birthDate: string;
  startDate = new Date();
  isPatient = false;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<HealthCenterPopupComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              public authService: AuthService) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.birthDate = '';
    console.log(this.item);

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        f_firstName: [this.item.firstName, [Validators.required]],
        f_lastName: [this.item.lastName, [Validators.required]],
        f_ssn: [this.item.ssn, [Validators.required]],
        f_isHospitalized: [this.item.hospitalized, [Validators.required]],
        f_isInICU: [this.item.icu, [Validators.required]],
        f_country: [this.item.country, [Validators.required]],
        f_region: [this.item.region, [Validators.required]],
        f_nationality: [this.item.nationality, [Validators.required]],
        f_sex: [this.item.sex, [Validators.required]],
      });

      // Enable patient controls
      if (this.data.isPatient) {
        this.isPatient = true;
      }

      // Disable SSN edit. Set default values in remaiming form elements
      this._elementForm.get('f_ssn').disable();
      this.startDate = new Date(this.item.birthDate);
      this.birthDate = formatDate(this.startDate, 'yyyy-MM-dd', 'en');
      this.region = this.item.region;
      this.country = this.item.country;
      this.nationality = this.item.nationality;
      this.sex = this.item.sex;
      if (this.data.isPatient) {
        this.isPatient = true;
      }
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        f_firstName: ['', [Validators.required]],
        f_lastName: ['', [Validators.required]],
        f_ssn: ['', [Validators.required]],
        f_isHospitalized: ['', [Validators.required]],
        f_isInICU: ['', [Validators.required]],
        f_country: ['', [Validators.required]],
        f_region: ['', [Validators.required]],
        f_nationality: ['', [Validators.required]],
        f_sex: ['', [Validators.required]],
        f_email: ['', [Validators.required]]
      });
    }

    // Fetch data
    this.getCountries();
  }

  /**
   * Closes dialog and forces refresh on parent table data
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
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
      });
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('p1') as HTMLInputElement).value = '';
    (document.getElementById('p2') as HTMLInputElement).value = '';
    (document.getElementById('p6') as HTMLInputElement).value = '';
  }

  /**
   * Resets password in Auth service
   */
  resetPassword() {
    Swal.fire({
      title: 'are you sure?',
      text: 'you won\'t be able to revert this!',
      icon: 'warning',
      customClass: {
        popup: 'container-alert'
      },
      showCancelButton: true,
      confirmButtonColor: '#43c59e',
      cancelButtonColor: '#d33',
      confirmButtonText: 'yes, reset!',
      cancelButtonText: 'cancel'
    }).then((result) => {
      if (result.value) {
        this.authService.ForgotPassword(this.data.email)
      }
    })
  }

  /**
   * Set country
   * @param event selected country
   */
  selectedCountry(event) {
    this.country = event.value;
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
   * Set sex
   * @param event selected sex
   */
  selectedSex(event) {
    this.sex = event.value;
  }

  /**
   * Set birth date
   * @param dateObject selected date
   */
  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.birthDate = stringified.substring(1, 11);
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const pFirstName = (document.getElementById('p1') as HTMLInputElement).value;
    const pLastName = (document.getElementById('p2') as HTMLInputElement).value;
    const pSsn = (document.getElementById('p3') as HTMLInputElement).value;
    const pHospitalized = (document.getElementById('p4') as HTMLInputElement).checked;
    const pIcu = (document.getElementById('p5') as HTMLInputElement).checked;

    // tslint:disable-next-line:max-line-length
    if (pFirstName !== '' && pLastName !== '' && this.birthDate !== '' && this.country !== '' && this.region !== '' && this.nationality !== '' && this.sex !== ''){
      if (this.type === 'add') {
        if (pSsn !== ''){
          axios.post(environment.serverURL + 'Patient', {
            ssn: pSsn,
            firstName: pFirstName,
            lastName: pLastName,
            birthDate: this.birthDate,
            hospitalized: pHospitalized,
            icu: pIcu,
            country: this.country,
            region: this.region,
            nationality: this.nationality,
            hospital: localStorage.getItem('hospitalId'),
            sex: this.sex
          }, {
            headers: {
              'Content-Type': 'application/json; charset=UTF-8'
            }
          })
            .then(response => {
              console.log(response);
              this.closeDialogRefresh();
            })
            .catch(error => {
              console.log(error.response);
            });
        }
      } else {
        axios.put(environment.serverURL + 'Patient/' + this.item.ssn, {
          ssn: -1,
          firstName: pFirstName,
          lastName: pLastName,
          birthDate: this.birthDate,
          hospitalized: pHospitalized,
          icu: pIcu,
          country: this.country,
          region: this.region,
          nationality: this.nationality,
          hospital: localStorage.getItem('hospitalId'),
          sex: this.sex
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
          })
          .catch(error => {
            console.log(error.response);
          });
      }
    }
  }
}


