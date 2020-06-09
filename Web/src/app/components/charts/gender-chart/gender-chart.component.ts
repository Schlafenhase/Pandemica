import { Component, OnInit } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { gender as data} from 'src/assets/data';

@Component({
  selector: 'app-gender-chart',
  templateUrl: './gender-chart.component.html',
  styleUrls: ['./gender-chart.component.scss']
})
export class GenderChartComponent implements OnInit {
  data: any[];
  view: any[] = [600, 500];

  // options
  gradient = false;
  showLegend = false;
  showLabels = false;
  isDoughnut = true;
  legendPosition = 'below';

  colorScheme = {
    domain: ['#43C59E', '#80CED7', '#43C59E', '#80CED7']
  };

  constructor() {
    Object.assign(this, { data });
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
