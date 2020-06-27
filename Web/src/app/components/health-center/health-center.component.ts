import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth/auth.service';
import {HealthCenter} from '../../services/data/users';
import {NetworkService} from '../../services/network/network.service';
import {MatDialog} from '@angular/material/dialog';
import {HealthCenterPopupComponent} from './health-center-popup/health-center-popup.component';
import {ContactsComponent} from './contacts/contacts.component';
import {ReportsService} from '../../services/health-center/reports.service';

@Component({
  selector: 'app-health-center',
  templateUrl: './health-center.component.html',
  styleUrls: ['./health-center.component.scss']
})
export class HealthCenterComponent implements OnInit {
  tableData = [{id: 117650424, name: 'kevin', brand: 'villager', category: 'Gamer', description: 'He really likes games'}];
  reportType: string;
  user: any;
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(public authService: AuthService,
              private networkService: NetworkService,
              private reportsService: ReportsService,
              private dialog?: MatDialog) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('userData')) as HealthCenter;
  }

  /**
   * Generates report depending on selected value of radio button
   */
  generateReport() {
    const mediaType = 'application/pdf';
    this.reportsService.GetReport(this.reportType).subscribe(
      content => {
        const blob = new Blob([content], {type: mediaType});
        const fileURL = URL.createObjectURL(blob);
        window.open(fileURL);
      });
  }

  /**
   * Detects changes in radio buttons
   * @param value report type to update
   */
  onItemChange(value) {
    this.reportType = value;
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
   * Deletes element in table with HTML entry values
   */
  deleteElement(item) {
    const dataToSend = {
      idNumber: item.id,
      fullName: '',
      brand: '',
      category: '',
      description: ''
    }
  }

  /**
   * Opens add/edit po-pup window
   * @param popUpType pop-up to open
   * @param sentItem item to send to pop-up
   */
  openPopUp(popUpType: string, sentItem) {
    // Call dialogRef to open window.
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(HealthCenterPopupComponent, {
      data: {
        type: popUpType,
        item: sentItem
      },
    });
  }

  /**
   * Opens contacts table pop-up window
   */
  openContactPopUp(popUpType: string, sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(ContactsComponent, {
      data: {
        type: popUpType,
        item: sentItem,
        id: sentItem.id,
      },
    });
  }

  /**
   * Controls contact pop-up behaviour
   */
  contactElement(item){
    this.openContactPopUp('contact', item);
    this.closePopUp()
  }

}
