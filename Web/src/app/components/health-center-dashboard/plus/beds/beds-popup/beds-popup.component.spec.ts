import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BedsPopupComponent } from './beds-popup.component';

describe('BedsPopupComponent', () => {
  let component: BedsPopupComponent;
  let fixture: ComponentFixture<BedsPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BedsPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BedsPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
