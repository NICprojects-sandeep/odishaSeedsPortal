import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaLayoutComponent } from './odia-layout.component';

describe('OdiaLayoutComponent', () => {
  let component: OdiaLayoutComponent;
  let fixture: ComponentFixture<OdiaLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
