import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';
import {EquipmentPopupComponent} from './equipment-popup/equipment-popup.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.scss']
})

export class EquipmentComponent implements OnInit {
  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    this.getEquipment();
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
        this.getEquipment();
      }
    });
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.secondWaveURL + 'Equipment/' + item.Id, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.getEquipment();
        this.fireSucessAlert();
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
    localStorage.setItem('equipmentId', item.Id);
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
      title: 'error',
      showConfirmButton: false,
      timer: 1000,
      customClass: {
        popup: 'container-alert'
      }
    })
  }

  /**
   * Displays success alert
   */
  fireSucessAlert(){
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'Operation done. You\'re awesome!',
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
  getEquipment() {
    axios.get(environment.secondWaveURL + 'Equipment', {
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
    this.dialogRef = this.dialog.open(EquipmentPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }

}
