import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegionsPopupComponent } from './regions-popup.component';

describe('RegionsPopupComponent', () => {
  let component: RegionsPopupComponent;
  let fixture: ComponentFixture<RegionsPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegionsPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegionsPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
