import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-health-center',
  templateUrl: './health-center.component.html',
  styleUrls: ['./health-center.component.scss']
})
export class HealthCenterComponent implements OnInit {
  reportType: string;

  constructor() { }

  ngOnInit(): void {
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
