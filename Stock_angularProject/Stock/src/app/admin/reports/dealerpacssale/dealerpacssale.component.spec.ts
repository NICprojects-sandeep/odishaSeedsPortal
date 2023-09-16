import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerpacssaleComponent } from './dealerpacssale.component';

describe('DealerpacssaleComponent', () => {
  let component: DealerpacssaleComponent;
  let fixture: ComponentFixture<DealerpacssaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DealerpacssaleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DealerpacssaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
