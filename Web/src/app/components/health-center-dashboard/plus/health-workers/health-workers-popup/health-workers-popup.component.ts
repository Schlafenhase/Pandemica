import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';

@Component({
  selector: 'app-health-workers-popup',
  templateUrl: './health-workers-popup.component.html',
  styleUrls: ['./health-workers-popup.component.scss']
})
export class HealthWorkersPopupComponent implements OnInit {

  public _elementForm: FormGroup;
  type: string;
  item: any;
  birthday: any;
  entryDate: any;
  selectedWorkerType:any;
  workerTypes = [
    'doctor',
    'nurse'
  ];
  isDoctor = false;
  title = 'health worker'

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<HealthWorkersPopupComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.entryDate = '';
    this.birthday = '';

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        ID: [this.item.id],
        wName: [this.item.wName, [Validators.required]],
        wLast: [this.item.wLast, [Validators.required]],
        wId: [this.item.wId, [Validators.required]],
        wPhone: [this.item.wPhone, [Validators.required]],
        wAddress: [this.item.wAddress, [Validators.required]],
        inDate: [this.item.selectedDate2, [Validators.required]],
        birthday: [this.item.selectedDate, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        ID: [''],
        wName: ['', [Validators.required]],
        wLast: ['', [Validators.required]],
        wId: ['', [Validators.required]],
        wPhone: ['', [Validators.required]],
        wAddress: ['', [Validators.required]],
        inDate: ['', [Validators.required]],
        birthday: ['', [Validators.required]],
      });
    }

    // If doctor is watching, disable stuff that he/she can't change
    if (this.data.isDoctor) {
      this.isDoctor = true;
      this.title = 'doctor profile';
    }
  }

  /**
   * Select the start date
   * @param dateObject selected date
   */
  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.birthday = stringified.substring(1, 11);
  }

  /**
   * Select the final date
   * @param dateObject selected date
   */
  updateDOB2(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.entryDate = stringified.substring(1, 11);
  }

  /**
   * Refreshes pop-up window data
   */
  emptyEntryData() {
    // Empty entries
    (document.getElementById('w1') as HTMLInputElement).value = '';
    (document.getElementById('w2') as HTMLInputElement).value = '';
    (document.getElementById('w3') as HTMLInputElement).value = '';
    (document.getElementById('w4') as HTMLInputElement).value = '';
    (document.getElementById('w5') as HTMLInputElement).value = '';
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    const wName = (document.getElementById('w1') as HTMLInputElement).value;
    const wLast = (document.getElementById('w2') as HTMLInputElement).value;
    const wId = (document.getElementById('w3') as HTMLInputElement).value;
    const wPhone = (document.getElementById('w4') as HTMLInputElement).value;
    const wAddress = (document.getElementById('w5') as HTMLInputElement).value;

    if (wName !== '' && wLast !== ''&& wId !== '' && wPhone !== '' && wAddress !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'Workers', {
          id: -1,
          name: wName,
          lastname: wLast,
          identification: wId,
          phone: wPhone,
          address: wAddress,
          birthday: this.birthday,
          entryDay: this.entryDate
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
        axios.put(environment.serverURL + 'Health-Workers/' + localStorage.getItem('healthworkerId'), {
          id: -1,
          name: wName,
          lastname: wLast,
          identification: wId,
          phone: wPhone,
          address: wAddress,
          birthday: this.birthday,
          entryDay: this.entryDate
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






