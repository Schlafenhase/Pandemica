import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import {PathologiesPopupComponent} from './pathologies-popup/pathologies-popup.component'
import { MatDialog } from '@angular/material/dialog';
import {NetworkService} from '../../../../services/network/network.service';
import {MedicationPopupComponent} from '../medication/medication-popup/medication-popup.component';

@Component({
  selector: 'app-pathologies',
  templateUrl: './pathologies.component.html',
  styleUrls: ['./pathologies.component.scss']
})
export class PathologiesComponent implements OnInit {
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
      fullName: '',
      brand: '',
      category: '',
      description: ''
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
    this.dialogRef = this.dialog.open(PathologiesPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }
}
