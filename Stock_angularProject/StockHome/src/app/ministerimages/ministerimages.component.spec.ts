import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinisterimagesComponent } from './ministerimages.component';

describe('MinisterimagesComponent', () => {
  let component: MinisterimagesComponent;
  let fixture: ComponentFixture<MinisterimagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MinisterimagesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MinisterimagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
