import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog} from '@angular/material/dialog';
import {ReservationsPopupComponent} from './reservations-popup/reservations-popup.component';
import {ReservationsPopupProceduresComponent} from './reservations-popup-procedures/reservations-popup-procedures.component';

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
  }

  /**
   * Gets data from server
   */
  getReservations() {
    // GET
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
        item: sentItem
      },
    });
  }
  openPopUpProcedures(sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(ReservationsPopupProceduresComponent, {
      data: {
        item: sentItem
      },
    });
  }

}
