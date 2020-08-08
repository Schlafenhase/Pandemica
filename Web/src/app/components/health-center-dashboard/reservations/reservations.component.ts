import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog} from '@angular/material/dialog';
import {ReservationsPopupComponent} from './reservations-popup/reservations-popup.component';
import {ReservationsPopupProceduresComponent} from './reservations-popup-procedures/reservations-popup-procedures.component';
import axios from 'axios';
import {environment} from '../../../../environments/environment';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {
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
    this.getReservations();

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
        this.getReservations()
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
    axios.delete(environment.storeProceduresURL + 'Reservation/' + item.Reservation, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.getReservations();
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Gets data from server
   */
  getReservations() {
    axios.get(environment.storeProceduresURL + 'Reservation/' + this.item.ssn, {
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
      });
  }

  /**
   * Opens pop-up window
   */
  openPopUp(popUpType: string, sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(ReservationsPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem,
        id: this.item.ssn
      },
    });
  }
  openPopUpProcedures(sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(ReservationsPopupProceduresComponent, {
      panelClass: 'custom-dialog',
      data: {
        item: sentItem,
        id: this.item.ssn
      },
    });
  }

}
