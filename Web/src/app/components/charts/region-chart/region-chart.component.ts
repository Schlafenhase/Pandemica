import {Component, Input, NgModule, OnInit} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import {ChartsService} from '../../../services/charts/charts.service';

@Component({
  selector: 'app-region-chart',
  templateUrl: './region-chart.component.html',
  styleUrls: ['./region-chart.component.scss']
})
export class RegionChartComponent implements OnInit {

  data: any[];

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
    this.data = [
      {
        name: 'active',
        value: 5430
      },
      {
        name: 'recovered',
        value: 1540
      },
      {
        name: 'dead',
        value: 440
      }
    ];
  }
}
