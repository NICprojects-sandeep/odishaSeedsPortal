import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankstatusComponent } from './bankstatus.component';

describe('BankstatusComponent', () => {
  let component: BankstatusComponent;
  let fixture: ComponentFixture<BankstatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BankstatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BankstatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
