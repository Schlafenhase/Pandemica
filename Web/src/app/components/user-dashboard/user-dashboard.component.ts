import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { HealthCenter, User } from '../../services/data/users';
import { PatientPathologiesComponent } from '../health-center-dashboard/patient-pathologies/patient-pathologies.component';
import { MatDialog } from '@angular/material/dialog';
import {MedicationsComponent} from '../health-center-dashboard/medications/medications.component';
import {HealthCenterPopupComponent} from '../health-center-dashboard/health-center-popup/health-center-popup.component';
import {ReservationsComponent} from '../health-center-dashboard/reservations/reservations.component';
import {MedicalHistoryComponent} from '../health-center-dashboard/medical-history/medical-history.component';

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
   * Opens pop-up window with type
   */
  openPopUp(popUpType: string) {
    this.isPopupOpened = true;
    switch (popUpType) {
      case 'edit-patient':
        this.dialogRef = this.dialog.open(HealthCenterPopupComponent, {
          data: {
            type: 'edit',
            id: this.user.ssn,
            fname: this.user.name,
            lname: this.user.lastName,
          },
        });
        break
      case 'medication':
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
        break
      case 'pathologies':
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
        break
      case 'reservations':
        this.dialogRef = this.dialog.open(ReservationsComponent, {
          panelClass: 'custom-dialog',
          data: {
            ssn: this.user.ssn,
            fname: this.user.name,
            lname: this.user.lastName,
            viewOnly: true
          },
        });
        break
      case 'medical-history':
        this.dialogRef = this.dialog.open(MedicalHistoryComponent, {
          panelClass: 'custom-dialog',
          data: {
            ssn: this.user.ssn,
            fname: this.user.name,
            lname: this.user.lastName,
            viewOnly: true
          },
        });
        break;
    }
  }

}
