import {Component, NgModule, OnInit} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { region as data } from 'src/assets/data';

@Component({
  selector: 'app-region-chart',
  templateUrl: './region-chart.component.html',
  styleUrls: ['./region-chart.component.scss']
})
export class RegionChartComponent implements OnInit {

  data: any[];

  view: any[] = [600, 500];

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
    domain: ['#43C59E', '#80CED7', '#F61067', '#F0D980']
  };

  constructor() {
    Object.assign(this, { data });
  }

  ngOnInit(): void {
  }

  onSelect(event) {
    console.log(event);
  }
}
