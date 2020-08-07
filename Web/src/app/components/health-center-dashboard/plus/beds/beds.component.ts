import { Component, OnInit } from '@angular/core';
import {NetworkService} from '../../../../services/network/network.service';
import {MatDialog} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';
import {BedsPopupComponent} from './beds-popup/beds-popup.component';
import { SwalPortalTargets } from '@sweetalert2/ngx-sweetalert2';
import {MatSnackBar} from '@angular/material/snack-bar';
import Swal from 'sweetalert2';

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
    private _snackBar: MatSnackBar,
    private networkService: NetworkService,
    private dialog?: MatDialog,
  ) {
  }

  ngOnInit(): void {
    axios.get(environment.secondWaveURL + 'Bed', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.tableData = response.data;
        this.fireSuccesAlert()
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
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
        window.location.reload();
        this.fireSuccesAlert()
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
  }
  addEquipment(){
    this.openEquipmentPopup();
    this.closePopUp()
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
  openEquipmentPopup() {
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(BedsPopupComponent)
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

