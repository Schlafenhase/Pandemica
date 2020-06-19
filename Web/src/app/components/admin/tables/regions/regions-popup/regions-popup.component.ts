import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';

@Component({
  selector: 'app-regions-popup',
  templateUrl: './regions-popup.component.html',
  styleUrls: ['./regions-popup.component.scss']
})
export class RegionsPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<RegionsPopupComponent>,
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
        Brand: [this.item.brand, [Validators.required]],
        Name: [this.item.name, [Validators.required]],
        Category: [this.item.category, [Validators.required]],
        Description: [this.item.description, [Validators.required]],
        Country: [this.item.country, [Validators.required]],
        Region: [this.item.region, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        Brand: ['', [Validators.required]],
        Name: ['', [Validators.required]],
        Category: ['', [Validators.required]],
        Description: ['', [Validators.required]],
        Country: ['', [Validators.required]],
        Region: ['', [Validators.required]],
      });
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('r1') as HTMLInputElement).value = '';
    (document.getElementById('r2') as HTMLInputElement).value = '';
    (document.getElementById('r3') as HTMLInputElement).value = '';
    (document.getElementById('r4') as HTMLInputElement).value = '';
    (document.getElementById('r5') as HTMLInputElement).value = '';
    (document.getElementById('r6') as HTMLInputElement).value = '';
    (document.getElementById('r7') as HTMLInputElement).value = '';
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
        name: this.data.name,
        brand: this.data.id.brand,
        category: this.data.category,
        description: this.data.description
      }

      url = '' // INSERT ADD URL
    } else {
      // Send selected item number to update in database
      dataToSend = {
        idNumber: this.item.id,
        name: this.data.name,
        brand: this.data.id.brand,
        category: this.data.category,
        description: this.data.description
      }

      url = '' // INSERT EDIT URL
    }

    // Send data to server
    // this.networkService.post(url, dataToSend)

    // Close popup window
    window.location.reload();
  }
}
