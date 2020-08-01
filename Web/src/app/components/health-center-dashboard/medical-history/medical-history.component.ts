import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../environments/environment';
import {MedicalHistoryPopupComponent} from './medical-history-popup/medical-history-popup.component';

@Component({
  selector: 'app-medical-history',
  templateUrl: './medical-history.component.html',
  styleUrls: ['./medical-history.component.scss']
})

export class MedicalHistoryComponent implements OnInit {
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
    this.patientName = this.data.fname + ' ' + this.data.lname;

    // Fetch data
    this.getMedicalHistory();

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
        this.getMedicalHistory()
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
  getMedicalHistory() {
    // GET
  }

  /**
   * Opens pop-up window
   */
  openPopUp(popUpType: string, sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(MedicalHistoryPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }

}
