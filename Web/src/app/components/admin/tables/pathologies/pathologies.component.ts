import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import {PathologiesPopupComponent} from './pathologies-popup/pathologies-popup.component'
import { MatDialog } from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import {MedicationPopupComponent} from '../medication/medication-popup/medication-popup.component';
import {environment} from '../../../../../environments/environment';

@Component({
  selector: 'app-pathologies',
  templateUrl: './pathologies.component.html',
  styleUrls: ['./pathologies.component.scss']
})
export class PathologiesComponent implements OnInit {
  tableData = [{}];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(
    private networkService: NetworkService,
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    axios.get(environment.serverURL + 'Pathology', {
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
    localStorage.setItem('pathologyID', item.id);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.serverURL + 'Pathology/' + item.id, {
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
    this.dialogRef = this.dialog.open(PathologiesPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }
}
