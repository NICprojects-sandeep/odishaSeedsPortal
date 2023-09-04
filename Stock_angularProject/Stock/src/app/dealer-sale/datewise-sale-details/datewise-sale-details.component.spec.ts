import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatewiseSaleDetailsComponent } from './datewise-sale-details.component';

describe('DatewiseSaleDetailsComponent', () => {
  let component: DatewiseSaleDetailsComponent;
  let fixture: ComponentFixture<DatewiseSaleDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatewiseSaleDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatewiseSaleDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
