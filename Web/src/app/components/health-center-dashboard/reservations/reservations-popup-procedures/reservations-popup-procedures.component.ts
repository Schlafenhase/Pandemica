import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog} from '@angular/material/dialog';
import {ReservationsPopupComponent} from '../reservations-popup/reservations-popup.component';
import {ReservationsPopupProceduresFormComponent} from './reservations-popup-procedures-form/reservations-popup-procedures-form.component';
import axios from "axios";
import {environment} from '../../../../../environments/environment';
import Swal from "sweetalert2";

@Component({
  selector: 'app-reservations-popup-procedures',
  templateUrl: './reservations-popup-procedures.component.html',
  styleUrls: ['./reservations-popup-procedures.component.scss']
})
export class ReservationsPopupProceduresComponent implements OnInit {

  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;
  patientName: string;
  item: any;
  viewOnly = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog?: MatDialog
  ) { }

  ngOnInit(): void {
    this.item = this.data.item;
    this.patientName = this.item.firstName + ' ' + this.item.lastName;

    // Fetch data
    this.getProcedures();

    // Activate view only mode
    if (this.data.viewOnly) {
      this.viewOnly = true
    }
  }

  /**
   * Adds element in table with HTML entry values
   */
  addElement() {
    this.openPopUp('add', null);
    this.closePopUp()
  }

  /**
   * Closes pop-up window
   */
  closePopUp() {
    // Call dialogRef when window is closed.
    this.dialogRef.afterClosed().subscribe(result => {
      this.isPopupOpened = false;

      if (result !== undefined) {
        this.getProcedures()
      }
    });
  }

  /**
   * Edits element in table with HTML entry values
   */
  editElement(item) {
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.storeProceduresURL + 'ReservationProcedure/' + this.item.Reservation + '/' + item.Procedure, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.getProcedures();
        this.fireSuccessAlert();
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
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
   * Gets data from server
   */
  getProcedures() {
    axios.get(environment.storeProceduresURL + 'ReservationProcedure/' + this.data.id + '/' + this.item.Reservation, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.tableData = response.data;
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
  }

  /**
   * Opens pop-up window
   */
  openPopUp(popUpType: string, sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(ReservationsPopupProceduresFormComponent, {
      data: {
        type: popUpType,
        item: sentItem,
        reservationId: this.item.Reservation
      },
    });
  }

}
