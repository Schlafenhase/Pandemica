import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BedequipmentPopupComponent } from './bedequipment-popup.component';

describe('BedequipmentPopupComponent', () => {
  let component: BedequipmentPopupComponent;
  let fixture: ComponentFixture<BedequipmentPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BedequipmentPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BedequipmentPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
