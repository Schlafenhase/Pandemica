import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PathologiesPopupComponent } from './pathologies-popup.component';

describe('PathologiesPopupComponent', () => {
  let component: PathologiesPopupComponent;
  let fixture: ComponentFixture<PathologiesPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PathologiesPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PathologiesPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
