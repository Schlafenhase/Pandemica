import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';

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
              private dialogRef: MatDialogRef<ContactsUpgradeComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    this.item = this.data.item;

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
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('password') as HTMLInputElement).value = '';
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

  }

}
