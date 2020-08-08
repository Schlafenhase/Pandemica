import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';
import Swal from "sweetalert2";

@Component({
  selector: 'app-states-popup',
  templateUrl: './states-popup.component.html',
  styleUrls: ['./states-popup.component.scss']
})
export class StatesPopupComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  states: string[];
  state: '';
  date: string;
  startDate = new Date();

  constructor(private _formBuilder: FormBuilder,
              private dialogRef: MatDialogRef<StatesPopupComponent>,
              private networkService: NetworkService,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    // Assign form type 'add' or 'edit'
    this.type = this.data.type;
    this.item = this.data.item;
    this.date = '';

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        f_status: [this.item.name, [Validators.required]]
      });

      this.startDate = new Date(this.item.date);
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        f_status: ['', [Validators.required]]
      });
    }
    this.getStates();
  }

  /**
   * Closes dialog and forces refresh on parent table data
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
  }

  /**
   * Fire error alert
   */
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'error',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Fire success alert
   */
  fireSuccessAlert(){
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'Everything went smoothly',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Get states from the database
   */
  getStates() {
    axios.get(environment.serverURL + 'State/Names', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.states = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Set state
   * @param event selected state
   */
  selectedState(event) {
    this.state = event.value;
  }

  /**
   * Set date
   * @param dateObject selected date
   */
  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.date = stringified.substring(1, 11);
  }

  /**
   * Updates changes in server depending on popup type
   */
  submit() {
    if (this.state !== '' && this.date !== ''){
      if (this.type === 'add') {
        axios.post(environment.serverURL + 'PatientState', {
          name: this.state,
          date: this.date,
          patientSsn: localStorage.getItem('patientSsn')
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.fireSuccessAlert();
            this.closeDialogRefresh();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      } else {
        axios.put(environment.serverURL + 'PatientState/' + localStorage.getItem('patientSsn') + '/' + localStorage.getItem('stateName') + '/' + localStorage.getItem('stateDate'), {
          name: this.state,
          date: this.date
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.fireSuccessAlert();
            this.closeDialogRefresh();
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      }
    }
  }

}
