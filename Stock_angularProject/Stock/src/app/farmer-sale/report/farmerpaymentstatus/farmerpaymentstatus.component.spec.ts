import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerpaymentstatusComponent } from './farmerpaymentstatus.component';

describe('FarmerpaymentstatusComponent', () => {
  let component: FarmerpaymentstatusComponent;
  let fixture: ComponentFixture<FarmerpaymentstatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerpaymentstatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmerpaymentstatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
