import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientPathologiesComponent } from './patient-pathologies.component';

describe('PatientPathologiesComponent', () => {
  let component: PatientPathologiesComponent;
  let fixture: ComponentFixture<PatientPathologiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientPathologiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientPathologiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
