import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationsPopupProceduresFormComponent } from './reservations-popup-procedures-form.component';

describe('ReservationsPopupProceduresFormComponent', () => {
  let component: ReservationsPopupProceduresFormComponent;
  let fixture: ComponentFixture<ReservationsPopupProceduresFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReservationsPopupProceduresFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationsPopupProceduresFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
