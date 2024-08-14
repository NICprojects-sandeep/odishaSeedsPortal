import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiacrriComponent } from './odiacrri.component';

describe('OdiacrriComponent', () => {
  let component: OdiacrriComponent;
  let fixture: ComponentFixture<OdiacrriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiacrriComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiacrriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
