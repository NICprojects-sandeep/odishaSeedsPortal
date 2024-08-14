import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiacontactusComponent } from './odiacontactus.component';

describe('OdiacontactusComponent', () => {
  let component: OdiacontactusComponent;
  let fixture: ComponentFixture<OdiacontactusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiacontactusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiacontactusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
