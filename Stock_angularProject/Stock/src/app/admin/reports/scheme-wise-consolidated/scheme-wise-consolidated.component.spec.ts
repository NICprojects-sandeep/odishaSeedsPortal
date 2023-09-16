import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchemeWiseConsolidatedComponent } from './scheme-wise-consolidated.component';

describe('SchemeWiseConsolidatedComponent', () => {
  let component: SchemeWiseConsolidatedComponent;
  let fixture: ComponentFixture<SchemeWiseConsolidatedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchemeWiseConsolidatedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchemeWiseConsolidatedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
