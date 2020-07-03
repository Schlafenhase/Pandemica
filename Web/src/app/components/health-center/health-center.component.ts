import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth/auth.service';
import {HealthCenter} from '../../services/data/users';
import {NetworkService} from '../../services/network/network.service';
import {MatDialog} from '@angular/material/dialog';
import {HealthCenterPopupComponent} from './health-center-popup/health-center-popup.component';
import {ContactsComponent} from './contacts/contacts.component';
import {ReportsService} from '../../services/health-center/reports.service';
import axios from 'axios';
import {environment} from '../../../environments/environment';
import {StatesComponent} from './states/states.component';
import {MedicationsComponent} from './medications/medications.component';
import {PatientPathologiesComponent} from './patient-pathologies/patient-pathologies.component';

@Component({
  selector: 'app-health-center',
  templateUrl: './health-center.component.html',
  styleUrls: ['./health-center.component.scss']
})
export class HealthCenterComponent implements OnInit {
  tableData = [{}];
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

    axios.post(environment.serverURL + 'Hospital/Email', {
      id: -1,
      name: '',
      phone: -1,
      managerName: '',
      capacity: -1,
      icuCapacity: -1,
      country: '',
      region: '',
      eMail: this.user.email
    }, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.user.uid = response.data[0].id;
        this.user.email = response.data[0].eMail;
        this.user.name = response.data[0].name;
        this.user.phone = response.data[0].phone;
        this.user.managerName = response.data[0].managerName;
        this.user.capacity = response.data[0].capacity;
        this.user.icuCapacity = response.data[0].icuCapacity;
        this.user.country = response.data[0].country;
        this.user.region = response.data[0].region;
        this.getPatients();
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  /**
   * Get patients from the database
   */
  getPatients(){
    axios.get(environment.serverURL + 'Patient/Hospital/' + this.user.uid, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.tableData = response.data;
        localStorage.setItem('hospitalId', this.user.uid);
      })
      .catch(error => {
        console.log(error.response);
      });
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
    localStorage.setItem('patientSsn', item.ssn);
    this.openPopUp('edit', item);
    this.closePopUp()
  }

  /**
   * Deletes element in table with HTML entry values
   */
  deleteElement(item) {
    axios.delete(environment.serverURL + 'Patient/' + item.ssn, {
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
        id: sentItem.ssn,
        fname: sentItem.firstName,
        lname: sentItem.lastName,
      },
    });
  }

  /**
   * Controls contact pop-up behaviour
   */
  contactElement(item){
    this.openContactPopUp('contacts', item);
    this.closePopUp()
  }

  /**
   * Opens contacts table pop-up window
   */
  openStatePopUp(popUpType: string, sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(StatesComponent, {
      data: {
        type: popUpType,
        item: sentItem,
        id: sentItem.ssn,
        fname: sentItem.firstName,
        lname: sentItem.lastName,
      },
    });
  }

  /**
   * Controls contact pop-up behaviour
   */
  stateElement(item){
    this.openStatePopUp('states', item);
    this.closePopUp()
  }

  /**
   * Opens contacts table pop-up window
   */
  openMedicationPopUp(popUpType: string, sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(MedicationsComponent, {
      data: {
        type: popUpType,
        item: sentItem,
        id: sentItem.ssn,
        fname: sentItem.firstName,
        lname: sentItem.lastName,
      },
    });
  }

  /**
   * Controls contact pop-up behaviour
   */
  medicationElement(item){
    this.openMedicationPopUp('medications', item);
    this.closePopUp()
  }

  /**
   * Opens contacts table pop-up window
   */
  openPathologyPopUp(popUpType: string, sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(PatientPathologiesComponent, {
      data: {
        type: popUpType,
        item: sentItem,
        id: sentItem.ssn,
        fname: sentItem.firstName,
        lname: sentItem.lastName,
      },
    });
  }

  /**
   * Controls contact pop-up behaviour
   */
  pathologyElement(item){
    this.openPathologyPopUp('pathologies', item);
    this.closePopUp()
  }

}
