import {Component, Input, NgModule, OnInit, Output} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import {ChartsService} from '../../../services/charts/charts.service';

@Component({
  selector: 'app-region-chart',
  templateUrl: './region-chart.component.html',
  styleUrls: ['./region-chart.component.scss']
})
export class RegionChartComponent implements OnInit {

  @Input() data: any[];

  @Input() view: any[] = [500, 400];

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

  constructor(private chartsService: ChartsService) {
    Object.assign(this);
  }

  ngOnInit(): void {
  }

  onSelect(event) {
    console.log(event);
  }
}
