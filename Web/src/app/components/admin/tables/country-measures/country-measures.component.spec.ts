import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CountryMeasuresComponent } from './country-measures.component';

describe('CountryMeasuresComponent', () => {
  let component: CountryMeasuresComponent;
  let fixture: ComponentFixture<CountryMeasuresComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CountryMeasuresComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountryMeasuresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
