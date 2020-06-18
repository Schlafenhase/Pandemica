import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthCentersTablePopupComponent } from './health-centers-table-popup.component';

describe('HealthCentersTablePopupComponent', () => {
  let component: HealthCentersTablePopupComponent;
  let fixture: ComponentFixture<HealthCentersTablePopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthCentersTablePopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthCentersTablePopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
