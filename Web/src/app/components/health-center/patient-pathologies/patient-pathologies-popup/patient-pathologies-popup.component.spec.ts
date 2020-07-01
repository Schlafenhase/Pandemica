import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientPathologiesPopupComponent } from './patient-pathologies-popup.component';

describe('PatientPathologiesPopupComponent', () => {
  let component: PatientPathologiesPopupComponent;
  let fixture: ComponentFixture<PatientPathologiesPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientPathologiesPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientPathologiesPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
