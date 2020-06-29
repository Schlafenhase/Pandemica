import {Component, Input, OnInit} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import {ChartsService} from '../../../services/charts/charts.service';

@Component({
  selector: 'app-gender-chart',
  templateUrl: './gender-chart.component.html',
  styleUrls: ['./gender-chart.component.scss']
})
export class GenderChartComponent implements OnInit {
  data: any[];
  @Input() view: any[] = [600, 400];

  // options
  gradient = false;
  showLegend = false;
  showLabels = false;
  isDoughnut = true;
  legendPosition = 'below';

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
    this.data = [
      {
        name: 'female',
        value: data.femaleCases
      },
      {
        name: 'male',
        value: data.maleCases
      }
    ];
  }
}
