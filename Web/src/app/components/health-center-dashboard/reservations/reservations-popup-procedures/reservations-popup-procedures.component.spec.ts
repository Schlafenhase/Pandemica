import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationsPopupProceduresComponent } from './reservations-popup-procedures.component';

describe('ReservationsPopupProceduresComponent', () => {
  let component: ReservationsPopupProceduresComponent;
  let fixture: ComponentFixture<ReservationsPopupProceduresComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReservationsPopupProceduresComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationsPopupProceduresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
