import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthWorkersPopupComponent } from './health-workers-popup.component';

describe('HealthWorkersPopupComponent', () => {
  let component: HealthWorkersPopupComponent;
  let fixture: ComponentFixture<HealthWorkersPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthWorkersPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthWorkersPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
