import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthCenterPopupComponent } from './health-center-popup.component';

describe('HealthCenterPopupComponent', () => {
  let component: HealthCenterPopupComponent;
  let fixture: ComponentFixture<HealthCenterPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthCenterPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthCenterPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
