import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';
@Component({
  selector: 'app-medication-popup',
  templateUrl: './medication-popup.component.html',
  styleUrls: ['./medication-popup.component.scss']
})
export class MedicationPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<MedicationPopupComponent>,
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
        medication: [this.item.medication, [Validators.required]],
        mHouse: [this.item.mHouse, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        medication: ['', [Validators.required]],
        mHouse: ['', [Validators.required]],
      });
    }
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('m1') as HTMLInputElement).value = '';
    (document.getElementById('m2') as HTMLInputElement).value = '';
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
        medication: this.data.medication,
        mHouse: this.data.id.mHouse,
      }

      url = '' // INSERT ADD URL
    } else {
      // Send selected item number to update in database
      dataToSend = {
        idNumber: this.item.id,
        medication: this.data.medication,
        mHouse: this.data.id.mHouse,
      }

      url = '' // INSERT EDIT URL
    }

    // Send data to server
    // this.networkService.post(url, dataToSend)

    // Close popup window
    window.location.reload();
  }
}
