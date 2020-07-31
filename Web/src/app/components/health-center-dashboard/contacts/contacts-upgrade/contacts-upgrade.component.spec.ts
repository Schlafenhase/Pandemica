import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactsUpgradeComponent } from './contacts-upgrade.component';

describe('ContactsUpgradeComponent', () => {
  let component: ContactsUpgradeComponent;
  let fixture: ComponentFixture<ContactsUpgradeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContactsUpgradeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactsUpgradeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
