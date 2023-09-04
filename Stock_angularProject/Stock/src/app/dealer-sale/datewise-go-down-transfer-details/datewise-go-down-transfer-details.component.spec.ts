import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatewiseGoDownTransferDetailsComponent } from './datewise-go-down-transfer-details.component';

describe('DatewiseGoDownTransferDetailsComponent', () => {
  let component: DatewiseGoDownTransferDetailsComponent;
  let fixture: ComponentFixture<DatewiseGoDownTransferDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatewiseGoDownTransferDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatewiseGoDownTransferDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
