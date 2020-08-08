import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Doctor } from '../../services/data/users';
import { MatDialog } from '@angular/material/dialog';
import { PatientPathologiesComponent } from '../health-center-dashboard/patient-pathologies/patient-pathologies.component';
import { MedicationsComponent } from '../health-center-dashboard/medications/medications.component';
import { StatesComponent } from '../health-center-dashboard/states/states.component';
import { ContactsComponent } from '../health-center-dashboard/contacts/contacts.component';
import {MedicalHistoryComponent} from '../health-center-dashboard/medical-history/medical-history.component';
import axios from 'axios';
import {environment} from '../../../environments/environment';
import {HealthWorkersPopupComponent} from '../health-center-dashboard/plus/health-workers/health-workers-popup/health-workers-popup.component';
import {ReservationsComponent} from '../health-center-dashboard/reservations/reservations.component';

@Component({
  selector: 'app-doctor-dashboard',
  templateUrl: './doctor-dashboard.component.html',
  styleUrls: ['./doctor-dashboard.component.scss']
})
export class DoctorDashboardComponent implements OnInit {
  user: Doctor;
  currentWindowWidth: number;
  tableData = [];
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(public authService: AuthService,
              private dialog?: MatDialog
  ) { }

  ngOnInit(): void {
    // Set user data as a doctor
    this.user = JSON.parse(localStorage.getItem('userData')) as Doctor;

    // Set initial window width
    this.currentWindowWidth = window.innerWidth;

    axios.post(environment.secondWaveURL + 'HealthWorker/Email', {
      Email: this.user.email
    }, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.user.ssn = response.data.Ssn;
        this.user.name = response.data.FirstName;
        this.user.lastName = response.data.LastName;
        this.user.phone = response.data.Phone;
        this.user.birthdate = response.data.BirthDate;
        this.user.role = response.data.Role;
        this.user.hospitalId = response.data.HospitalId;
        this.user.address = response.data.Address;
        this.user.startdate = response.data.StartDate;
        this.user.sex = response.data.Sex;
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
    axios.get(environment.serverURL + 'Patient/Hospital/' + this.user.hospitalId, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        this.tableData = response.data;
        localStorage.setItem('hospitalId', this.user.hospitalId);
      })
      .catch(error => {
        console.log(error.response);
      });
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
   * Open a pre-determined pop-up window
   */
  openPopUpType(type: string, sentItem: any) {
    this.isPopupOpened = true;
    switch (type) {
      case 'contacts':
        this.dialogRef = this.dialog.open(ContactsComponent, {
          panelClass: 'custom-dialog',
          data: {
            item: sentItem
          },
        });
        break;
      case 'states':
        this.dialogRef = this.dialog.open(StatesComponent, {
          panelClass: 'custom-dialog',
          data: {
            item: sentItem,
            id: sentItem.ssn,
            fname: sentItem.firstName,
            lname: sentItem.lastName,
          },
        });
        break;
      case 'medication':
        this.dialogRef = this.dialog.open(MedicationsComponent, {
          panelClass: 'custom-dialog',
          data: {
            item: sentItem,
            id: sentItem.ssn,
            fname: sentItem.firstName,
            lname: sentItem.lastName,
          },
        });
        break;
      case 'pathologies':
        this.dialogRef = this.dialog.open(PatientPathologiesComponent, {
          panelClass: 'custom-dialog',
          data: {
            item: sentItem,
            id: sentItem.ssn,
            fname: sentItem.firstName,
            lname: sentItem.lastName,
          },
        });
        break;
      case 'medical-history':
        this.dialogRef = this.dialog.open(MedicalHistoryComponent, {
          panelClass: 'custom-dialog',
          data: {
            item: sentItem
          },
        });
        break;
      case 'reservations':
        this.dialogRef = this.dialog.open(ReservationsComponent, {
          panelClass: 'custom-dialog',
          data: {
            item: sentItem
          },
        });
        break;
      case 'edit-doctor':
        this.dialogRef = this.dialog.open(HealthWorkersPopupComponent, {
          data: {
            item: this.user,
            type: 'edit',
            isDoctor: true
          },
        });
        break;
    }
    this.closePopUp();
  }

  /**
   * Listen for real time window resizing
   */
  @HostListener('window:resize')
  onResize() {
    this.currentWindowWidth = window.innerWidth
  }

}
