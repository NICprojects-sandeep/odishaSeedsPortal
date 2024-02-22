import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubsidyinvolovementComponent } from './subsidyinvolovement.component';

describe('SubsidyinvolovementComponent', () => {
  let component: SubsidyinvolovementComponent;
  let fixture: ComponentFixture<SubsidyinvolovementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubsidyinvolovementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubsidyinvolovementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
