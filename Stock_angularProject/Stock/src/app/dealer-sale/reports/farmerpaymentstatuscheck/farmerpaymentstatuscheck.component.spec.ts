import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerpaymentstatuscheckComponent } from './farmerpaymentstatuscheck.component';

describe('FarmerpaymentstatuscheckComponent', () => {
  let component: FarmerpaymentstatuscheckComponent;
  let fixture: ComponentFixture<FarmerpaymentstatuscheckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerpaymentstatuscheckComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmerpaymentstatuscheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
