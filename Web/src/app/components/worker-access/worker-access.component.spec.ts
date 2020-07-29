import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkerAccessComponent } from './worker-access.component';

describe('WorkerAccessComponent', () => {
  let component: WorkerAccessComponent;
  let fixture: ComponentFixture<WorkerAccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkerAccessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkerAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
