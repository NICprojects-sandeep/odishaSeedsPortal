import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaministerimagesComponent } from './odiaministerimages.component';

describe('OdiaministerimagesComponent', () => {
  let component: OdiaministerimagesComponent;
  let fixture: ComponentFixture<OdiaministerimagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaministerimagesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaministerimagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
