import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import {PatientStatusPopupComponent} from './patient-status-popup/patient-status-popup.component'
import { MatDialog } from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import {PathologiesPopupComponent} from '../pathologies/pathologies-popup/pathologies-popup.component';
import {environment} from '../../../../../environments/environment';

@Component({
  selector: 'app-patient-status',
  templateUrl: './patient-status.component.html',
  styleUrls: ['./patient-status.component.scss']
})
export class PatientStatusComponent implements OnInit {
  staticStates = [{}];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(
    private networkService: NetworkService,
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    axios.get(environment.serverURL + 'State', {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.staticStates = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
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
    });
  }

  /**
   * Edits element in table with HTML entry values
   */
  editElement(item) {
    localStorage.setItem('statusId', item.id);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.serverURL + 'State/' + item.id, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        window.location.reload();
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
    this.dialogRef = this.dialog.open(PatientStatusPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }
}
