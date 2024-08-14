import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaheaderComponent } from './odiaheader.component';

describe('OdiaheaderComponent', () => {
  let component: OdiaheaderComponent;
  let fixture: ComponentFixture<OdiaheaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaheaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaheaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
