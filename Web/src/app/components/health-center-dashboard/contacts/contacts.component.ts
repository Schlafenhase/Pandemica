import {Component, Inject, OnInit} from '@angular/core';
import {NetworkService} from '../../../services/network/network.service';
import {MAT_DIALOG_DATA, MatDialog} from '@angular/material/dialog';
import {ContactsPopupComponent} from './contacts-popup/contacts-popup.component';
import axios from 'axios';
import {environment} from '../../../../environments/environment';
import {ContactsUpgradeComponent} from './contacts-upgrade/contacts-upgrade.component';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})
export class ContactsComponent implements OnInit {
  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;
  patientID: any;
  patientName: any;

  constructor(
    private networkService: NetworkService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog?: MatDialog
  ) {
  }

  ngOnInit(): void {
    // Assign patient ID of contacts to incoming data ID
    this.patientID = this.data.id;
    localStorage.setItem('patientSsn', this.patientID);
    this.patientName = this.data.fname + ' ' + this.data.lname;
    this.getContacts();
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

      if (result !== undefined) {
        if (result.event !== 'upgrade-contact') {
          this.getContacts();
        } else {
          this.closeDialogRefresh();
        }
      }
    });
  }

  /**
   * Edits element in table with HTML entry values
   */
  editElement(item) {
    localStorage.setItem('personSsn', item.ssn);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Deletes element in table with HTMl entry data
   */
  deleteElement(item) {
    axios.delete(environment.serverURL + 'Contact/' + item.ssn + '/' + this.patientID, {
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
  getContacts() {
    axios.get(environment.serverURL + 'Contact/Patient/' + this.patientID, {
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
   * Opens pop-up window
   */
  openPopUp(popUpType: string, sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    if (popUpType === 'upgrade-contact') {
      this.dialogRef = this.dialog.open(ContactsUpgradeComponent, {
        data: {
          item: sentItem
        },
      });
    } else {
      this.dialogRef = this.dialog.open(ContactsPopupComponent, {
        data: {
          type: popUpType,
          item: sentItem
        },
      });
    }
  }

  /**
   * Opens pop-up to upgrade patient with log-in info
   */
  upgradeElement(item) {
    this.openPopUp('upgrade-contact', item);
    this.closePopUp()
  }
}

