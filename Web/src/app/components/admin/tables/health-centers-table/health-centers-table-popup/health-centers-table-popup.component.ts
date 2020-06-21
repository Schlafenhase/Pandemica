import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';
import {MatSelectChange} from '@angular/material/select';
import {MatOption} from '@angular/material/core';

@Component({
  selector: 'app-health-centers-table-popup',
  templateUrl: './health-centers-table-popup.component.html',
  styleUrls: ['./health-centers-table-popup.component.scss']
})
export class HealthCentersTablePopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  countrySelected: any;
  states: string[] = [
    'Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware',
    'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky',
    'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi',
    'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico',
    'New York', 'North Carolina', 'North Dakota', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania',
    'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
    'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
  ];

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
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('h2') as HTMLInputElement).value = '';
    (document.getElementById('h3') as HTMLInputElement).value = '';
    (document.getElementById('h4') as HTMLInputElement).value = '';
    (document.getElementById('h5') as HTMLInputElement).value = '';
    (document.getElementById('h6') as HTMLInputElement).value = '';
    (document.getElementById('h7') as HTMLInputElement).value = '';
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
        Country: this.data.countrySelected,
        Region: this.data.Region,
        Name: this.data.Name,
        bCapacity: this.data.bCapacity,
        bAmount: this.data.bAmount,
        hDirector: this.data.hDirector,
        dContact: this.data.dContact,
      }

      url = '' // INSERT ADD URL
    } else {
      // Send selected item number to update in database
      dataToSend = {
        idNumber: this.item.id,
        Country: this.data.countrySelected,
        Region: this.data.Region,
        Name: this.data.Name,
        bCapacity: this.data.bCapacity,
        bAmount: this.data.bAmount,
        hDirector: this.data.hDirector,
        dContact: this.data.dContact,
      }

      url = '' // INSERT EDIT URL
    }

    // Send data to server
    // this.networkService.post(url, dataToSend)

    // Close popup window
    window.location.reload();
  }
}
