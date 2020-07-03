import {Component, Input, NgModule, OnInit} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import {ChartsService} from '../../../services/charts/charts.service';
import {IHomeView} from '../../../services/data/users';

@Component({
  selector: 'app-patients-chart',
  templateUrl: './patients-chart.component.html',
  styleUrls: ['./patients-chart.component.scss']
})
export class PatientsChartComponent implements OnInit {

  data: any[];

  @Input() view: any[] = [600, 400];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = false;
  showXAxisLabel = false;
  xAxisLabel = 'location';
  showYAxisLabel = false;
  yAxisLabel = 'population';

  colorScheme = {
    domain: ['#43C59E', '#80CED7', '#43C59E', '#80CED7']
  };

  constructor(private chartsService: ChartsService) {
    Object.assign(this);
  }

  ngOnInit(): void {
    this.chartsService.chartsData$().subscribe(
      data =>{
        this.parseData(data)
      }
    );
  }

  onSelect(event) {
    console.log(event);
  }

  private parseData(data: any) {
    if (!data) return;
    this.data = [
      {
        name: 'at home',
        value: data.patientsAtHome
      },
      {
        name: 'hospitalized',
        value: data.patientsHospitalized
      },
      {
        name: 'icu',
        value: data.patientsInICU
      }
    ];
  }
}
