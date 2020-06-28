import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthCentersTableComponent } from './health-centers-table.component';

describe('HealthCentersTableComponent', () => {
  let component: HealthCentersTableComponent;
  let fixture: ComponentFixture<HealthCentersTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthCentersTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthCentersTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
