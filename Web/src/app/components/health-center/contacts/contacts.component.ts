import {Component, Inject, OnInit} from '@angular/core';
import {NetworkService} from '../../../services/network/network.service';
import {MAT_DIALOG_DATA, MatDialog} from '@angular/material/dialog';
import {ContactsPopupComponent} from './contacts-popup/contacts-popup.component';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})
export class ContactsComponent implements OnInit {
  tableData = [{id: 117650424, name: 'kevin', brand: 'villager', category: 'Gamer', description: 'He really likes games'}];
  isPopupOpened: boolean;
  dialogRef: any;
  patientID: any;

  constructor(
    private networkService: NetworkService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    // Assign patient ID of contacts to incoming data ID
    this.patientID = this.data.id;
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
    this.dialogRef = this.dialog.open(ContactsPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }
}

