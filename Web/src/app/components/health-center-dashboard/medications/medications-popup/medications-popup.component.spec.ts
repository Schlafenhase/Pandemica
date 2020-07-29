import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicationsPopupComponent } from './medications-popup.component';

describe('MedicationsPopupComponent', () => {
  let component: MedicationsPopupComponent;
  let fixture: ComponentFixture<MedicationsPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MedicationsPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicationsPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
