import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CountryMeasuresPopupComponent } from './country-measures-popup/country-measures-popup.component'
import { MatDialog } from '@angular/material/dialog';
import { NetworkService } from '../../../../services/network/network.service';
import axios from 'axios';
import {environment} from '../../../../../environments/environment';


@Component({
  selector: 'app-country-measures',
  templateUrl: './country-measures.component.html',
  styleUrls: ['./country-measures.component.scss']
})
export class CountryMeasuresComponent implements OnInit {
  tableData = [{}];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(
    private networkService: NetworkService,
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    axios.get(environment.serverURL + 'Enforces/Name', {
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
    localStorage.setItem('enforcesId', item.id);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Opens pop-up window
   */
  openPopUp(popUpType: string, sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(CountryMeasuresPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }
}

