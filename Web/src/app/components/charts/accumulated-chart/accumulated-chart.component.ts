import {Component, NgModule, OnInit} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { accumulated as data } from 'src/assets/data';

@Component({
  selector: 'app-accumulated-chart',
  templateUrl: './accumulated-chart.component.html',
  styleUrls: ['./accumulated-chart.component.scss']
})
export class AccumulatedChartComponent implements OnInit {

  data: any[];
  view: any[] = [1200, 600];

  // options
  legend = false;
  showLabels = true;
  animations = true;
  xAxis = true;
  yAxis = true;
  showYAxisLabel = false;
  showXAxisLabel = false;
  xAxisLabel = 'days';
  yAxisLabel = 'Population';
  timeline = true;

  colorScheme = {
    domain: ['#43C59E', '#80CED7', '#F61067', '#F0D980']
  };

  constructor() {
    Object.assign(this, {data});
  }

  ngOnInit(): void {
  }

  onSelect(event): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(event)));
  }

  onActivate(event): void {
    console.log('Activate', JSON.parse(JSON.stringify(event)));
  }

  onDeactivate(event): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(event)));
  }
}
