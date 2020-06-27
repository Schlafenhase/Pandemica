import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CountryMeasuresPopupComponent } from './country-measures-popup.component';

describe('CountryMeasuresPopupComponent', () => {
  let component: CountryMeasuresPopupComponent;
  let fixture: ComponentFixture<CountryMeasuresPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CountryMeasuresPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountryMeasuresPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
