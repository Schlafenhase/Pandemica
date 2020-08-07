import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';
import {ProceduresPopupComponent} from './procedures-popup/procedures-popup.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-procedures',
  templateUrl: './procedures.component.html',
  styleUrls: ['./procedures.component.scss']
})

export class ProceduresComponent implements OnInit {
  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(
    private dialog?: MatDialog
  ) { }

  ngOnInit(): void {
    this.getProcedures();
  }

  /**
   * Adds element in table with HTML entry values
   */
  addElement() {
    this.openPopUp('add', null);
    this.closePopUp();
  }

  /**
   * Closes pop-up window
   */
  closePopUp() {
    // Call dialogRef when window is closed.
    this.dialogRef.afterClosed().subscribe(result => {
      this.isPopupOpened = false;

      // Refresh data if information has been added or updated
      if (result !== undefined) {
        this.getProcedures();
      }
    });
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.storeProceduresURL + 'Procedure/' + localStorage.getItem('hospitalId') + '/' + item.Id, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.getProcedures();
        this.fireSuccessAlert()
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert()
      });
  }

  /**
   * Edits element in table with HTML entry values
   */
  editElement(item) {
    localStorage.setItem('procedureId', item.Id);
    this.openPopUp('edit', item);
    this.closePopUp();
  }

  /**
   * Displays error alert
   */
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'Hmm... it seems there was a problem',
      showConfirmButton: false,
      timer: 2000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Displays success alert
   */
  fireSuccessAlert(){
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'We got you, everything ready!',
      showConfirmButton: false,
      timer: 2000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Fetch data from server
   */
  getProcedures() {
    axios.get(environment.storeProceduresURL + 'Procedure/' + localStorage.getItem('hospitalId'), {
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
        this.fireErrorAlert()
      });
  }

  /**
   * Opens pop-up window
   */
  openPopUp(popUpType: string, sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(ProceduresPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }

}
