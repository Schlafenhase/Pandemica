import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';
import {LoungesPopupComponent} from './lounges-popup/lounges-popup.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lounges',
  templateUrl: './lounges.component.html',
  styleUrls: ['./lounges.component.scss']
})

export class LoungesComponent implements OnInit {
  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    this.getLounges();
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

      // Refresh data if information has been added or updated
      if (result !== undefined) {
        this.getLounges();
      }
    });
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.secondWaveURL + 'Lounge/' + item.Number, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.getLounges();
        this.fireSuccessAlert();
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
  }

  /**
   * Edits element in table with HTML entry values
   */
  editElement(item) {
    localStorage.setItem('loungesId', item.Number);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Displays error alert
   */
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'something went wrong with that',
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
      title: 'Everything done in here!',
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
  getLounges() {
    axios.get(environment.secondWaveURL + 'Lounge/' + localStorage.getItem('hospitalId'), {
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
    this.dialogRef = this.dialog.open(LoungesPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }

}

