import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerpacspaymentComponent } from './dealerpacspayment.component';

describe('DealerpacspaymentComponent', () => {
  let component: DealerpacspaymentComponent;
  let fixture: ComponentFixture<DealerpacspaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DealerpacspaymentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DealerpacspaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
