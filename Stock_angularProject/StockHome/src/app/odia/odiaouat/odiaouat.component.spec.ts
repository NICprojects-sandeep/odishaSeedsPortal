import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaouatComponent } from './odiaouat.component';

describe('OdiaouatComponent', () => {
  let component: OdiaouatComponent;
  let fixture: ComponentFixture<OdiaouatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaouatComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaouatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
