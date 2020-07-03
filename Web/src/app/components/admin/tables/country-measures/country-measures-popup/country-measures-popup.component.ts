import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';

@Component({
  selector: 'app-country-measures-popup',
  templateUrl: './country-measures-popup.component.html',
  styleUrls: ['./country-measures-popup.component.scss']
})
export class CountryMeasuresPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  countries: string[] = [];
  sanitaryMeasurements: string[] = [];
  country: '';
  sanitaryMeasurement: '';
  mState: 0;
  startDate: string;
  finalDate: string;
  disableSelect = new FormControl(false);
  countrySelected: any;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<CountryMeasuresPopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.startDate = '';
    this.finalDate = '';

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        mName: [this.item.mName, [Validators.required]],
        mCountry: [this.item.mCountry, [Validators.required]],
        inDate: [this.item.selectedDate, [Validators.required]],
        outDate: [this.item.selectedDate2, [Validators.required]],
      });
      this.getCountries();
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        mName: ['', [Validators.required]],
        mCountry: ['', [Validators.required]],
        inDate: ['', [Validators.required]],
        outDate: ['', [Validators.required]],
      });
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
        this.getSanitaryMeasurements();
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Get sanitary measurements from the database
   */
  getSanitaryMeasurements() {
    axios.get(environment.serverURL + 'SanitaryMeasurements/Names', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.sanitaryMeasurements = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Select the start date
   * @param dateObject selected date
   */
  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.startDate = stringified.substring(1, 11);
  }

  /**
   * Select the final date
   * @param dateObject selected date
   */
  updateDOB2(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.finalDate = stringified.substring(1, 11);
  }

  /**
   * Select the country
   * @param event selected country
   */
  selectedCountry(event) {
    this.country = event.value;
  }

  /**
   * Select the sanitary measure
   * @param event selected sanitary measure
   */
  selectedSanitaryMeasurement(event) {
    this.sanitaryMeasurement = event.value;
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    if (this.country !== '' && this.sanitaryMeasurement !== '' && this.startDate !== '' && this.finalDate !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Enforces/Name', {
          country: this.country,
          startDate: this.startDate,
          finalDate: this.finalDate,
          id: -1,
          measurementName: this.sanitaryMeasurement
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
      } else {
        // Send selected item number to update in database
        axios.put(environment.serverURL + 'Enforces/Name/' + localStorage.getItem('enforcesId'), {
          country: this.country,
          startDate: this.startDate,
          finalDate: this.finalDate,
          id: -1,
          measurementName: this.sanitaryMeasurement
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
