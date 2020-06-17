import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth/auth.service';
import {HealthCenter} from '../../services/data/users';

@Component({
  selector: 'app-health-center',
  templateUrl: './health-center.component.html',
  styleUrls: ['./health-center.component.scss']
})
export class HealthCenterComponent implements OnInit {
  reportType: string;
  user: any;

  constructor(
    public authService: AuthService
  ) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('userData')) as HealthCenter;
  }

  /**
   * Generates report depending on selected value of radio button
   */
  generateReport() {
    // tslint:disable-next-line:triple-equals
    if (this.reportType == 'patientsByStatus') {
      // GENERATE PATIENTS BY STATUS REPORT
    } else {
      // GENERATE CASES & DEATHS LAST WEEK REPORT
    }
  }

  /**
   * Detects changes in radio buttons
   * @param value report type to update
   */
  onItemChange(value) {
    this.reportType = value;
  }

}
