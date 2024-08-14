import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiafooterComponent } from './odiafooter.component';

describe('OdiafooterComponent', () => {
  let component: OdiafooterComponent;
  let fixture: ComponentFixture<OdiafooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiafooterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiafooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
