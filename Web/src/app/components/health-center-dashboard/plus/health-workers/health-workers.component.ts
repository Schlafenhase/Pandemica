import { Component, OnInit } from '@angular/core';
import {NetworkService} from '../../../../services/network/network.service';
import {MatDialog} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';
import {BedsPopupComponent} from '../beds/beds-popup/beds-popup.component';
import {HealthWorkersPopupComponent} from './health-workers-popup/health-workers-popup.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-health-workers',
  templateUrl: './health-workers.component.html',
  styleUrls: ['./health-workers.component.scss']
})

export class HealthWorkersComponent implements OnInit {
  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(
    private networkService: NetworkService,
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    this.getHealthWorkers();
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
        this.getHealthWorkers();
      }
    });
  }

  /**
   * Edits element in table with HTML entry values
   */
  editElement(item) {
    localStorage.setItem('healthWorkerId', item.Ssn);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.secondWaveURL + 'HealthWorker/' + item.Ssn, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.getHealthWorkers();
        this.fireSuccessAlert();
      })
      .catch(error => {
        console.log(error.response);
        this.fireErrorAlert();
      });
  }

  /**
   * Displays error alert
   */
  fireErrorAlert() {
    // Fire alert
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: 'Having problems right now, try again please',
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
  getHealthWorkers() {
    axios.get(environment.secondWaveURL + 'HealthWorker/' + localStorage.getItem('hospitalId'), {
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
    this.dialogRef = this.dialog.open(HealthWorkersPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }

}
