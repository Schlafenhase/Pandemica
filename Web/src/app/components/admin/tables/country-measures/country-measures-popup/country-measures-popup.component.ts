import { Component, OnInit, Inject } from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';

@Component({
  selector: 'app-country-measures-popup',
  templateUrl: './country-measures-popup.component.html',
  styleUrls: ['./country-measures-popup.component.scss']
})
export class CountryMeasuresPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  states: string[] = [
    'Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware',
    'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky',
    'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi',
    'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico',
    'New York', 'North Carolina', 'North Dakota', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania',
    'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
    'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
  ];
  mState: 0;
  selectedDate: any;
  selectedDate2: any;
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
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        mName: ['', [Validators.required]],
        mCountry: ['', [Validators.required]],
        inDate: ['', [Validators.required]],
        outDate: ['', [Validators.required]],
      });
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('1') as HTMLInputElement).value = '';
  }

  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    const dob = stringified.substring(1, 11);
    this.selectedDate = dob;
  }
  updateDOB2(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    const dob2 = stringified.substring(1, 11);
    this.selectedDate2 = dob2;
  }
  selected(event) {
    console.log(event.value);
    this.countrySelected = event.value;
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    let url: string;
    let dataToSend: any;

    if (this.type === 'add') {
      // ID number is empty, it isn't assigned yet by database
      dataToSend = {
        idNumber: '',
        mName: this.data.mName,
        mCountry: this.data.mCountry,
        inDate: this.data.inDate,
        outDate: this.data.outDate
      }

      url = '' // INSERT ADD URL
    } else {
      // Send selected item number to update in database
      dataToSend = {
        idNumber: this.item.id,
        mName: this.data.mName,
        mCountry: this.data.mCountry,
        inDate: this.data.inDate,
        outDate: this.data.outDate,
      }

      url = '' // INSERT EDIT URL
    }

    // Send data to server
    // this.networkService.post(url, dataToSend)

    // Close popup window
    window.location.reload();
  }
}

// tslint:disable-next-line:max-line-length
// ESTE ARCHIVO .TS DEBE SER AJUSTADO CON PRUEBAS DEL SERVER YA QUE SE DEBE COMPROBAR EL FUNCIONAMIENTO DEL NGIF PARA LA ACTIVIDAD O INACTIVIDAD
