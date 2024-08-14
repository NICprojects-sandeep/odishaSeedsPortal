import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiacomponentoneComponent } from './odiacomponentone.component';

describe('OdiacomponentoneComponent', () => {
  let component: OdiacomponentoneComponent;
  let fixture: ComponentFixture<OdiacomponentoneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiacomponentoneComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiacomponentoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
