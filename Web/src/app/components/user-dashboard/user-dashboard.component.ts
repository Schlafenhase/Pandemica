import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { HealthCenter, User } from '../../services/data/users';
import { PatientPathologiesComponent } from '../health-center-dashboard/patient-pathologies/patient-pathologies.component';
import { MatDialog } from '@angular/material/dialog';
import {MedicationsComponent} from '../health-center-dashboard/medications/medications.component';
import {HealthCenterPopupComponent} from '../health-center-dashboard/health-center-popup/health-center-popup.component';
import {ReservationsComponent} from '../health-center-dashboard/reservations/reservations.component';
import {MedicalHistoryComponent} from '../health-center-dashboard/medical-history/medical-history.component';
import axios from 'axios';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.scss']
})
export class UserDashboardComponent implements OnInit {
  user: User;
  isPopupOpened: boolean;
  dialogRef: any;
  selectedCategory1: 5;
  categories1 = [
    1,
    2,
    3,
    4,
    5
  ];
  selectedCategory2: 5;
  categories2 = [
    1,
    2,
    3,
    4,
    5
  ];
  selectedCategory3: 5;
  categories3 = [
    1,
    2,
    3,
    4,
    5
  ];

  constructor(public authService: AuthService,
              private dialog?: MatDialog
              ) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('userData')) as User;

    axios.post(environment.secondWaveURL + 'Patient/Email', {
      Email: this.user.email
    }, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        if (response.data !== null){
          this.user.name = response.data.FirstName;
          this.user.lastName = response.data.LastName;
          this.user.ssn = response.data.Ssn;
          this.user.birthdate = response.data.BirthDate;
          this.user.isHospitalized = response.data.Hospitalized;
          this.user.isInICU = response.data.Icu;
          this.user.country = response.data.Country;
          this.user.region = response.data.Region;
          this.user.nationality = response.data.Nationality;
          this.user.sex = response.data.Sex;
        }
      })
      .catch(error => {
        console.log(error.response);
      });
  }

  closePopUp() {
    // Call dialogRef when window is closed.
    this.dialogRef.afterClosed().subscribe(result => {
      this.isPopupOpened = false;

      // Refresh patient list if information has been added or updated
      if (result !== undefined) {
        this.ngOnInit();
      }
    });
  }
  /*Get all the information for the feedback from the radio buttons*/
  submitFeedbackInfo() {
    const fCleanliness = this.selectedCategory1;
    const fService = this.selectedCategory2;
    const fPunctuality = this.selectedCategory3;

    // Verify input
    if (fCleanliness === undefined || fService === undefined || fPunctuality === undefined) {
      // Notiication. Please rate all elements.
    } else {
      console.log(fCleanliness, fService, fPunctuality)
    }

    axios.post(environment.serverURL + 'Feedback', {
      cleanliness: fCleanliness,
      service: fService,
      punctuality: fPunctuality
    }, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(response => {
        console.log(response);
        // Notification. Successfully posted rating.
      })
      .catch(error => {
        console.log(error.response);
      });
  }
  /**
   * Opens pop-up window with type
   */
  openPopUp(popUpType: string) {
    this.isPopupOpened = true;
    const jsonItem = {
      ssn: this.user.ssn,
      firstName: this.user.name,
      lastName: this.user.lastName,
      hospitalized: this.user.isHospitalized,
      icu: this.user.isInICU,
      country: this.user.country,
      region: this.user.region,
      nationality: this.user.nationality,
      sex: this.user.sex,
      birthDate: this.user.birthdate
    };
    switch (popUpType) {
      case 'edit-patient':
        this.dialogRef = this.dialog.open(HealthCenterPopupComponent, {
          data: {type: 'edit',
                item: jsonItem},
        });
        break;
      case 'medication':
        this.dialogRef = this.dialog.open(MedicationsComponent, {
          panelClass: 'custom-dialog',
          data: {
            type: 'medications',
            item: jsonItem,
            id: this.user.ssn,
            fname: this.user.name,
            lname: this.user.lastName,
            viewOnly: true
          },
        });
        break;
      case 'pathologies':
        this.dialogRef = this.dialog.open(PatientPathologiesComponent, {
          panelClass: 'custom-dialog',
          data: {
            type: 'pathologies',
            item: jsonItem,
            id: this.user.ssn,
            fname: this.user.name,
            lname: this.user.lastName,
            viewOnly: true
          },
        });
        break;
      case 'reservations':
        this.dialogRef = this.dialog.open(ReservationsComponent, {
          panelClass: 'custom-dialog',
          data: {
            type: 'reservations',
            item: jsonItem,
            ssn: this.user.ssn,
            fname: this.user.name,
            lname: this.user.lastName,
            viewOnly: true
          },
        });
        break;
      case 'medical-history':
        this.dialogRef = this.dialog.open(MedicalHistoryComponent, {
          panelClass: 'custom-dialog',
          data: {
            type: 'medical-history',
            item: jsonItem,
            ssn: this.user.ssn,
            fname: this.user.name,
            lname: this.user.lastName,
            viewOnly: true
          },
        });
        break;
    }
    this.closePopUp();
  }

}
