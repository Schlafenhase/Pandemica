import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoungesPopupComponent } from './lounges-popup.component';

describe('LoungesPopupComponent', () => {
  let component: LoungesPopupComponent;
  let fixture: ComponentFixture<LoungesPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoungesPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoungesPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
