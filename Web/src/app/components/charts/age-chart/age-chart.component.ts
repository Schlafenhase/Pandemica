import {Component, Input, NgModule, OnInit} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import {ChartsService} from '../../../services/charts/charts.service';

@Component({
  selector: 'app-age-chart',
  templateUrl: './age-chart.component.html',
  styleUrls: ['./age-chart.component.scss']
})
export class AgeChartComponent implements OnInit {

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
        name: '0 - 12',
        value: data['0-12']
      },
      {
        name: '13 - 20',
        value: data['13-20']
      },
      {
        name: '21 - 39',
        value: data['21-39']
      },
      {
        name: '40 - 59',
        value: data['40-59']
      },
      {
        name: '60 - 79',
        value: data['60-79']
      },
      {
        name: '+ 80',
        value: data['+80']
      }
    ];
  }
}
