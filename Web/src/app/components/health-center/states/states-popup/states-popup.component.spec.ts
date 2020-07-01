import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatesPopupComponent } from './states-popup.component';

describe('StatesPopupComponent', () => {
  let component: StatesPopupComponent;
  let fixture: ComponentFixture<StatesPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatesPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatesPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
