import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from "axios";
import {environment} from '../../../../../../environments/environment';
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
    const mName = (document.getElementById('m1') as HTMLInputElement).value;
    const mPharmacy = (document.getElementById('m2') as HTMLInputElement).value;

    if (mName !== '' && mPharmacy !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Medication', {
          id: -1,
          name: mName,
          pharmacy: mPharmacy
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
        axios.put(environment.serverURL + 'Medication/' + localStorage.getItem('medicationId'), {
          id: -1,
          name: mName,
          pharmacy: mPharmacy
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
