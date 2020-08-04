import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentPopupComponent } from './equipment-popup.component';

describe('EquipmentPopupComponent', () => {
  let component: EquipmentPopupComponent;
  let fixture: ComponentFixture<EquipmentPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
