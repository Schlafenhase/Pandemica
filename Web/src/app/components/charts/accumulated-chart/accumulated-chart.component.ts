import {Component, Input, NgModule, OnInit} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import {ChartsService} from '../../../services/charts/charts.service';

@Component({
  selector: 'app-accumulated-chart',
  templateUrl: './accumulated-chart.component.html',
  styleUrls: ['./accumulated-chart.component.scss']
})
export class AccumulatedChartComponent implements OnInit {

  data: any[];
  @Input() view: any[] = [1200, 500];

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

  onSelect(event): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(event)));
  }

  onActivate(event): void {
    console.log('Activate', JSON.parse(JSON.stringify(event)));
  }

  onDeactivate(event): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(event)));
  }

  private parseData(data: any) {
    const accumulated = data.accumulated;

    // For each day
    const seriesActive = [];
    const seriesDead = [];
    const seriesRecovered = [];
    accumulated.forEach(day => {
      seriesActive.push({
          name: day.date,
          value: day.active
        });
      seriesDead.push({
        name: day.date,
        value: day.dead
      });
      seriesRecovered.push({
        name: day.date,
        value: day.recovered
      });
    });

    // Updates the chart
    this.data = [
      {
        name: 'new cases',
        series: seriesActive
      },
      {
        name: 'recovered',
        series: seriesRecovered
      },
      {
        name: 'dead',
        series: seriesDead
      },
    ]
  }
}
