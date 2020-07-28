import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactsPopupComponent } from './contacts-popup.component';

describe('ContactsPopupComponent', () => {
  let component: ContactsPopupComponent;
  let fixture: ComponentFixture<ContactsPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContactsPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactsPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
