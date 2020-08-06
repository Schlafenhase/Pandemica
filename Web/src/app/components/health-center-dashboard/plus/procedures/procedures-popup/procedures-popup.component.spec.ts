import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProceduresPopupComponent } from './procedures-popup.component';

describe('ProceduresPopupComponent', () => {
  let component: ProceduresPopupComponent;
  let fixture: ComponentFixture<ProceduresPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProceduresPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProceduresPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
