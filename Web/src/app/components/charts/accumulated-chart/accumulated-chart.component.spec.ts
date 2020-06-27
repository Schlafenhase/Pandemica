import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccumulatedChartComponent } from './accumulated-chart.component';

describe('AccumulatedChartComponent', () => {
  let component: AccumulatedChartComponent;
  let fixture: ComponentFixture<AccumulatedChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccumulatedChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccumulatedChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
