import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaHeaderComponent } from './odia-header.component';

describe('OdiaHeaderComponent', () => {
  let component: OdiaHeaderComponent;
  let fixture: ComponentFixture<OdiaHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
