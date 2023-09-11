import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatewisegodowntransferdetailsComponent } from './datewisegodowntransferdetails.component';

describe('DatewisegodowntransferdetailsComponent', () => {
  let component: DatewisegodowntransferdetailsComponent;
  let fixture: ComponentFixture<DatewisegodowntransferdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatewisegodowntransferdetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatewisegodowntransferdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
