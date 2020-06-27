import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SanitaryMeasuresPopupComponent } from './sanitary-measures-popup.component';

describe('SanitaryMeasuresPopupComponent', () => {
  let component: SanitaryMeasuresPopupComponent;
  let fixture: ComponentFixture<SanitaryMeasuresPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SanitaryMeasuresPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SanitaryMeasuresPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
