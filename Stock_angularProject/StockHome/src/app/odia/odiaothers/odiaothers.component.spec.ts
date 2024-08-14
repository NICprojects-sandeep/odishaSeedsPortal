import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaothersComponent } from './odiaothers.component';

describe('OdiaothersComponent', () => {
  let component: OdiaothersComponent;
  let fixture: ComponentFixture<OdiaothersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaothersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaothersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
