import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalHistoryPopupComponent } from './medical-history-popup.component';

describe('MedicalHistoryPopupComponent', () => {
  let component: MedicalHistoryPopupComponent;
  let fixture: ComponentFixture<MedicalHistoryPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MedicalHistoryPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicalHistoryPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
