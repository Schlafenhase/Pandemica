import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import {MedicationPopupComponent} from './medication-popup/medication-popup.component'
import { MatDialog } from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import {HealthCentersTablePopupComponent} from '../health-centers-table/health-centers-table-popup/health-centers-table-popup.component';


@Component({
  selector: 'app-medication',
  templateUrl: './medication.component.html',
  styleUrls: ['./medication.component.scss']
})
export class MedicationComponent implements OnInit {
  tableData = [{id: 117650424, name: 'kevin', brand: 'villager', category: 'Gamer', description: 'He really likes games'}];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(
    private networkService: NetworkService,
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
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
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    const dataToSend = {
      idNumber: item.id,
      Medication: '',
      mHouse: '',
    }

    // Send data to server
    // this.networkService.post('', dataToSend)

    // Reload window to show changes
    window.location.reload();
  }

  /**
   * Opens pop-up window
   */
  openPopUp(popUpType: string, sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(MedicationPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }
}
