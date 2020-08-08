import {Component, Inject, OnInit} from '@angular/core';
import {AuthService} from '../../../../../services/auth/auth.service';
import {NetworkService} from '../../../../../services/network/network.service';
import {ReportsService} from '../../../../../services/health-center/reports.service';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import {HealthCenterPopupComponent} from '../../../health-center-popup/health-center-popup.component';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-reservations-popup-procedures-form',
  templateUrl: './reservations-popup-procedures-form.component.html',
  styleUrls: ['./reservations-popup-procedures-form.component.scss']
})
export class ReservationsPopupProceduresFormComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  procedure: any;
  procedures = [];
  title: string;

  constructor(
    private _formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ReservationsPopupProceduresFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    this.type = this.data.type;
    this.item = this.data.item;
    this.getProcedures();

    // Initialize Material form
    if (this.item != null) {
      // Item exists, edit mode.
      this._elementForm = this._formBuilder.group({
        pName: [this.item.pName, [Validators.required]],
      });
    } else {
      // Item does not exist, add mode.
      this._elementForm = this._formBuilder.group({
        pName: ['', [Validators.required]],
      });
    }
  }

  /**
   * Fetches data from server
   */
  getProcedures() {
    axios.get(environment.secondWaveURL + 'Procedure/Name', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.procedures = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Manages procedure selection in HTML
   */
  selectedProcedure(event){
    this.procedure = event.value;
  }

  /**
   * Closes the dialog on contact upgrade
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
  }

  /**
   * Fire error sweet alert
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
   * Fires success sweet alert
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

  submit(){
    if (this.procedure !== ''){
      if (this.type === 'add') {
        axios.post(environment.storeProceduresURL + 'ReservationProcedure', {
          ReservationId: this.data.reservationId,
          ProcedureName: this.procedure
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
            this.fireSuccessAlert()
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      } else {
        // Send selected item number to update in database
        axios.put(environment.storeProceduresURL + 'ReservationProcedure/' + this.data.reservationId, {
          OldProcedure: this.item.Procedure,
          NewProcedure: this.procedure
        }, {
          headers: {
            'Content-Type': 'application/json; charset=UTF-8'
          }
        })
          .then(response => {
            console.log(response);
            this.closeDialogRefresh();
            this.fireSuccessAlert()
          })
          .catch(error => {
            console.log(error.response);
            this.fireErrorAlert();
          });
      }
    }
  }
}
