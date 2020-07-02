import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';
import {MatSelectChange} from '@angular/material/select';
import {MatOption} from '@angular/material/core';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';

@Component({
  selector: 'app-health-centers-table-popup',
  templateUrl: './health-centers-table-popup.component.html',
  styleUrls: ['./health-centers-table-popup.component.scss']
})
export class HealthCentersTablePopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  countries: string[] = [];
  regions: string[] = [];
  country: '';
  region: '';

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<HealthCentersTablePopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        Country: [this.item.countrySelected, [Validators.required]],
        Region: [this.item.Region, [Validators.required]],
        Name: [this.item.Name, [Validators.required]],
        bCapacity: [this.item.bCapacity, [Validators.required]],
        bAmount: [this.item.bAmount, [Validators.required]],
        hDirector: [this.item.hDirector, [Validators.required]],
        dContact: [this.item.dContact, [Validators.required]],
      });
      this.getCountries();
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        Country: ['', [Validators.required]],
        Region: ['', [Validators.required]],
        Name: ['', [Validators.required]],
        bCapacity: ['', [Validators.required]],
        bAmount: ['', [Validators.required]],
        hDirector: ['', [Validators.required]],
        dContact: ['', [Validators.required]],
      });
      this.getCountries();
    }
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
    (document.getElementById('h1') as HTMLInputElement).value = '';
    (document.getElementById('h2') as HTMLInputElement).value = '';
    (document.getElementById('h3') as HTMLInputElement).value = '';
    (document.getElementById('h4') as HTMLInputElement).value = '';
    (document.getElementById('h5') as HTMLInputElement).value = '';
    (document.getElementById('h6') as HTMLInputElement).value = '';
  }

  selectedCountry(event) {
    this.country = event.value;
  }

  selectedRegion(event) {
    this.region = event.value;
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const rName = (document.getElementById('h1') as HTMLInputElement).value;
    const rPhone = (document.getElementById('h2') as HTMLInputElement).value;
    const rManagerName = (document.getElementById('h3') as HTMLInputElement).value;
    const rCapacity = (document.getElementById('h4') as HTMLInputElement).value;
    const rICUCapacity = (document.getElementById('h5') as HTMLInputElement).value;
    const rEmail = (document.getElementById('h6') as HTMLInputElement).value;

    // tslint:disable-next-line:max-line-length
    if (this.region !== '' && this.country !== '' && rName !== '' && rPhone !== '' && rManagerName !== '' && rCapacity !== '' && rICUCapacity !== '' && rEmail !== '') {
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Hospital', {
          id: -1,
          name: rName,
          phone: rPhone,
          managerName: rManagerName,
          capacity: rCapacity,
          icuCapacity: rICUCapacity,
          country: this.country,
          region: this.region,
          eMail: rEmail
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
        axios.put(environment.serverURL + 'Hospital/' + localStorage.getItem('hospitalId'), {
          id: -1,
          name: rName,
          phone: rPhone,
          managerName: rManagerName,
          capacity: rCapacity,
          icuCapacity: rICUCapacity,
          country: this.country,
          region: this.region,
          eMail: rEmail
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
