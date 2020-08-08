import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import Swal from 'sweetalert2';
import {BedquipmentFormPopupComponent} from './bedquipment-form-popup/bedquipment-form-popup.component';

@Component({
  selector: 'app-bedequipment-popup',
  templateUrl: './bedequipment-popup.component.html',
  styleUrls: ['./bedequipment-popup.component.scss']
})

export class BedequipmentPopupComponent implements OnInit {
  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;
  item: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    // Get item from data
    this.item = this.data.item;
    this.getEquipment();
    localStorage.setItem('bedNumber', this.item.Number);
  }

  /**
   * Adds element in table with HTML entry values
   */
  addElement() {
    this.openPopUp('add', null);
    this.closePopUp()
  }

  /**
   * Closes the dialog on contact upgrade
   */
  closeDialogRefresh() {
    this.dialogRef.close({event: 'refresh'});
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
        this.getEquipment();
      }
    });
  }

  /**
   * Edits element in table with HTML entry values
   */
  editElement(item) {
    localStorage.setItem('equipmentName', item.Name);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.storeProceduresURL + 'BedEquipment/' + this.item.Number + '/' + item.Id, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.getEquipment();
        this.fireSuccesAlert();
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
  }

  /**
   * Opens pop-up window
   */
  getEquipment() {
    axios.get(environment.storeProceduresURL + 'BedEquipment/' + this.item.Number, {
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
    this.dialogRef = this.dialog.open(BedquipmentFormPopupComponent, {
      data: {
        item: sentItem,
        type: popUpType
      },
    });
  }

  fireSuccesAlert(){
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
}

