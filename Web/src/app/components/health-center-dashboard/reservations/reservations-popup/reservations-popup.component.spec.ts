import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationsPopupComponent } from './reservations-popup.component';

describe('ReservationsPopupComponent', () => {
  let component: ReservationsPopupComponent;
  let fixture: ComponentFixture<ReservationsPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReservationsPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationsPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
