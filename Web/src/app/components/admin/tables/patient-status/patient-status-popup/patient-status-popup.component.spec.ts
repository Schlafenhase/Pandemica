import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientStatusPopupComponent } from './patient-status-popup.component';

describe('PatientStatusPopupComponent', () => {
  let component: PatientStatusPopupComponent;
  let fixture: ComponentFixture<PatientStatusPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientStatusPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientStatusPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
