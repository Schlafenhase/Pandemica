import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BedquipmentFormPopupComponent } from './bedquipment-form-popup.component';

describe('BedquipmentFormPopupComponent', () => {
  let component: BedquipmentFormPopupComponent;
  let fixture: ComponentFixture<BedquipmentFormPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BedquipmentFormPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BedquipmentFormPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
