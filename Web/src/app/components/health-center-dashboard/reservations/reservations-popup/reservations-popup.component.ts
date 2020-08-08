import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';

@Component({
  selector: 'app-reservations-popup',
  templateUrl: './reservations-popup.component.html',
  styleUrls: ['./reservations-popup.component.scss']
})
export class ReservationsPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  startDate: string;

  constructor(
    private _formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ReservationsPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.startDate = '';

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  /**
   * Closes dialog and forces refresh on parent table data
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
  }

  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.startDate = stringified.substring(1, 11);
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    if (this.startDate !== '') {
      if (this.type === 'add') {
        axios.post(environment.storeProceduresURL + 'Reservation', {
          PatientSsn: this.data.id,
          ReservationDate: this.startDate,
          HospitalId: localStorage.getItem('hospitalId')
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
      })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh()
          })
          .catch(error => {
            console.log(error.response);
          });
      } else {
        axios.put(environment.storeProceduresURL + 'Reservation/' + this.item.Reservation, {
          ReservationDate: this.startDate,
          HospitalId: localStorage.getItem('hospitalId')
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
