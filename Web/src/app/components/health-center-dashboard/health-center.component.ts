import {Component, HostListener, OnInit} from '@angular/core';
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
import {MedicalHistoryComponent} from './medical-history/medical-history.component';
import {ReservationsComponent} from './reservations/reservations.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-health-center',
  templateUrl: './health-center.component.html',
  styleUrls: ['./health-center.component.scss']
})
export class HealthCenterComponent implements OnInit {
  tableData = [];
  reportType: string;
  user: any;
  isPopupOpened: boolean;
  dialogRef: any;
  public currentWindowWidth: number;
  isHealthCenterPlus = false;

  constructor(public authService: AuthService,
              private networkService: NetworkService,
              private reportsService: ReportsService,
              private dialog?: MatDialog) { }

  ngOnInit(): void {
    const hcUser = JSON.parse(localStorage.getItem('userData')) as HealthCenter;
    this.user = hcUser;

    if (this.user.id === 0 || this.user.country === '') {
      // Get logged health center information
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
          // Update information in UI labels
          const hcData = response.data[0];

          // Update local user
          this.user.id = hcData.id;
          this.user.email = hcData.eMail;
          this.user.name = hcData.name;
          this.user.phone = hcData.phone;
          this.user.managerName = hcData.managerName;
          this.user.capacity = hcData.capacity;
          this.user.icuCapacity = hcData.icuCapacity;
          this.user.country = hcData.country;
          this.user.region = hcData.region;

          // Update global health center user
          const healthCenterData: HealthCenter = {
            uid: this.user.uid,
            id: hcData.id,
            email: hcData.eMail,
            name: hcData.name,
            phone: hcData.phone,
            emailVerified: this.user.emailVerified,
            managerName: hcData.managerName,
            capacity: hcData.capacity,
            icuCapacity: hcData.icuCapacity,
            country: hcData.country,
            region: hcData.region
          };
          localStorage.setItem('userData', JSON.stringify(healthCenterData));
          // Load patients table
          this.getPatients();
          // Activate HealthCenter+
          if (this.user.country === 'Costa Rica') {
            this.isHealthCenterPlus = true;
          }
        })
        .catch(error => {
          console.log(error.response);
        });
    }

    // Load patients table
    this.getPatients();
    // Activate HealthCenter+
    if (this.user.country === 'Costa Rica') {
      this.isHealthCenterPlus = true;
    }
    // Set initial window width
    this.currentWindowWidth = window.innerWidth;
  }

  /**
   * Get patients from the database
   */
  getPatients(){
    axios.get(environment.serverURL + 'Patient/Hospital/' + this.user.id, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.tableData = response.data;
        localStorage.setItem('hospitalId', this.user.id);
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

      // Refresh patient list if information has been added or updated
      if (result !== undefined) {
        this.getPatients();
      }
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
        this.getPatients();
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
      panelClass: 'responsive-dialog',
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
      panelClass: 'custom-dialog',
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
   * Opens medical history table pop-up window
   */
  openMedicalHistoryPopUp(popUpType: string, sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(MedicalHistoryComponent, {
      panelClass: 'custom-dialog',
      data: {
        type: popUpType,
        item: sentItem,
      },
    });
  }

  /**
   * Opens medical history table pop-up window
   */
  reservationsElement(sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(ReservationsComponent, {
      panelClass: 'custom-dialog',
      data: {
        item: sentItem,
      },
    });
    this.closePopUp();
  }

  /**
   * Controls contact pop-up behaviour
   */
  contactElement(item){
    this.openContactPopUp('contacts', item);
    this.closePopUp()
  }

  /**
   * Controls medical history pop-up behaviour
   */
  medicalHistoryElement(item){
    this.openMedicalHistoryPopUp('medical-history', item);
    this.closePopUp()
  }

  /**
   * Opens state table pop-up window
   */
  openStatePopUp(popUpType: string, sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(StatesComponent, {
      panelClass: 'custom-dialog',
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
   * Controls state pop-up behaviour
   */
  stateElement(item){
    this.openStatePopUp('states', item);
    this.closePopUp()
  }

  /**
   * Opens medication table pop-up window
   */
  openMedicationPopUp(popUpType: string, sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(MedicationsComponent, {
      panelClass: 'custom-dialog',
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
   * Controls medication pop-up behaviour
   */
  medicationElement(item){
    this.openMedicationPopUp('medications', item);
    this.closePopUp()
  }

  /**
   * Opens pathology table pop-up window
   */
  openPathologyPopUp(popUpType: string, sentItem){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(PatientPathologiesComponent, {
      panelClass: 'custom-dialog',
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
   * Controls pathology pop-up behaviour
   */
  pathologyElement(item){
    this.openPathologyPopUp('pathologies', item);
    this.closePopUp()
  }

  /**
   * Listen for real time window resizing
   */
  @HostListener('window:resize')
  onResize() {
    this.currentWindowWidth = window.innerWidth
  }

  deleteConfirmation(item){
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
      title: 'Are you sure?',
      text: 'You wont be able to revert this!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {
        swalWithBootstrapButtons.fire(
          'Deleted!',
          'Your file has been deleted.',
          'success'
        )
        this.deleteElement(item)
      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire(
          'Cancelled',
          'Your imaginary file is safe :)',
          'error'
        )
      }
    })
  }

}
