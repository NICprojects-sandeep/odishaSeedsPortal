import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OuatComponent } from './ouat.component';

describe('OuatComponent', () => {
  let component: OuatComponent;
  let fixture: ComponentFixture<OuatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OuatComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OuatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
