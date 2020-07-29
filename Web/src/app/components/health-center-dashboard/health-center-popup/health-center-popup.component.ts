import {Component, Inject, OnInit} from '@angular/core';
import {NetworkService} from '../../../services/network/network.service';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import axios from 'axios';
import {environment} from '../../../../environments/environment';

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

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<HealthCenterPopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.birthDate = '';

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        pName: [this.item.pName, [Validators.required]],
        pLast: [this.item.pLast, [Validators.required]],
        pAge: [this.item.pAge, [Validators.required]],
        pNationality: [this.item.pNationality, [Validators.required]],
        pRegion: [this.item.pRegion, [Validators.required]],
        pPathology: [this.item.pPathology, [Validators.required]],
        pState: [this.item.pState, [Validators.required]],
        pMedication: [this.item.pMedication, [Validators.required]],
      });
      (document.getElementById('p3') as HTMLInputElement).disabled = true;
      this.getCountries();
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        pName: ['', [Validators.required]],
        pLast: ['', [Validators.required]],
        pAge: ['', [Validators.required]],
        pNationality: ['', [Validators.required]],
        pRegion: ['', [Validators.required]],
        pPathology: ['', [Validators.required]],
        pState: ['', [Validators.required]],
        pMedication: ['', [Validators.required]],
      });
      (document.getElementById('p3') as HTMLInputElement).disabled = false;
      this.getCountries();
    }
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
    (document.getElementById('p3') as HTMLInputElement).value = '';
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
              window.location.reload();
            })
            .catch(error => {
              console.log(error.response);
            });
        }
      } else {
        axios.put(environment.serverURL + 'Patient/' + localStorage.getItem('patientSsn'), {
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
            window.location.reload();
          })
          .catch(error => {
            console.log(error.response);
          });
      }
    }
  }
}


