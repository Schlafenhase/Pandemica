import {Component, Inject, OnInit} from '@angular/core';
import {AuthService} from '../../../../../services/auth/auth.service';
import {NetworkService} from '../../../../../services/network/network.service';
import {ReportsService} from '../../../../../services/health-center/reports.service';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import axios from 'axios';
import {environment} from '../../../../../../environments/environment';
import {HealthCenterPopupComponent} from '../../../health-center-popup/health-center-popup.component';
import {FormBuilder, FormGroup} from '@angular/forms';

@Component({
  selector: 'app-reservations-popup-procedures-form',
  templateUrl: './reservations-popup-procedures-form.component.html',
  styleUrls: ['./reservations-popup-procedures-form.component.scss']
})
export class ReservationsPopupProceduresFormComponent implements OnInit {
  public _elementForm: FormGroup;
  type: string;
  item: any;
  name: any;
  names = [
    'Yamir',
    'Ale',
    'Kevin',
    'Jose'
  ];
  title: string;

  constructor(
    private _formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ReservationsPopupProceduresFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
  }

  updateDOB(dateObject): any {
    const stringified = JSON.stringify(dateObject.value);
    this.name = stringified.substring(1, 11);
  }
  submit(){

  }
}
