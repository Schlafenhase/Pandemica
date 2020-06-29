import { Injectable } from '@angular/core';
import {Subject} from 'rxjs';
import {IHomeView} from '../data/users';

@Injectable({
  providedIn: 'root'
})
export class ChartsService {

  private updateCharts = new Subject();

  constructor() { }

  pushChartsData(data: IHomeView) {
    this.updateCharts.next(data)
  }

  chartsData$() {
    return this.updateCharts.asObservable();
  }
}
