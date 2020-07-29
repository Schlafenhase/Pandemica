import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { HealthCenter, User } from '../../services/data/users';
import { PatientPathologiesComponent } from '../health-center-dashboard/patient-pathologies/patient-pathologies.component';
import { MatDialog } from '@angular/material/dialog';
import {MedicationsComponent} from '../health-center-dashboard/medications/medications.component';
import {HealthCenterPopupComponent} from '../health-center-dashboard/health-center-popup/health-center-popup.component';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.scss']
})
export class UserDashboardComponent implements OnInit {
  user: User;
  isPopupOpened: boolean;
  dialogRef: any;

  constructor(public authService: AuthService,
              private dialog?: MatDialog
              ) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('userData')) as User;
  }

  /**
   * Opens edit patient pop-up window
   */
  openEditPatientPopUp(){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(HealthCenterPopupComponent, {
      data: {
        type: 'edit',
        id: this.user.ssn,
        fname: this.user.name,
        lname: this.user.lastName,
      },
    });
  }

  /**
   * Opens medication table pop-up window in view-only mode
   */
  openMedicationPopUp(){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(MedicationsComponent, {
      panelClass: 'custom-dialog',
      data: {
        type: 'medication',
        id: this.user.ssn,
        fname: this.user.name,
        lname: this.user.lastName,
        viewOnly: true
      },
    });
  }

  /**
   * Opens pathology table pop-up window in view-only mode
   */
  openPathologyPopUp(){
    this.isPopupOpened = true;
    this.dialogRef = this.dialog.open(PatientPathologiesComponent, {
      panelClass: 'custom-dialog',
      data: {
        type: 'pathologies',
        id: this.user.ssn,
        fname: this.user.name,
        lname: this.user.lastName,
        viewOnly: true
      },
    });
  }

}
