import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import axios from 'axios';
import { environment } from '../../../../../environments/environment';
import { BedsPopupComponent } from './beds-popup/beds-popup.component';
import Swal from 'sweetalert2';
import { BedequipmentPopupComponent } from './bedequipment-popup/bedequipment-popup.component';

@Component({
  selector: 'app-beds',
  templateUrl: './beds.component.html',
  styleUrls: ['./beds.component.scss']
})

export class BedsComponent implements OnInit {
  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;

  public constructor(
    private dialog?: MatDialog,
  ) {
  }

  ngOnInit(): void {
    this.getBeds();
  }

  /**
   * Adds element in table with HTML entry values
   */
  addElement() {
    this.openPopUp('add', null);
    this.closePopUp()
  }

  /**
   * Opens equipment pop-up window
   */
  addEquipment(item: any){
    this.openEquipmentPopup(item);
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
        this.getBeds();
      }
    });
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.secondWaveURL + 'Bed/' + item.Number, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.getBeds();
        this.fireSuccessAlert()
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
    localStorage.setItem('bedId', item.Number);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Fires Success Alert
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
   * Fires Error Alert
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
   * Get data from server
   */
  getBeds() {
    axios.get(environment.secondWaveURL + 'Bed', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.tableData = response.data;
        this.fireSuccessAlert()
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
    this.dialogRef = this.dialog.open(BedsPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }

  /**
   * Opens equipment pop-up window
   */
  openEquipmentPopup(sentItem: any) {
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(BedequipmentPopupComponent, {
      panelClass: 'custom-dialog',
      data: {
        item: sentItem
      },
    });
  }
}

