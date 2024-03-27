import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaContactusComponent } from './odia-contactus.component';

describe('OdiaContactusComponent', () => {
  let component: OdiaContactusComponent;
  let fixture: ComponentFixture<OdiaContactusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaContactusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaContactusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
