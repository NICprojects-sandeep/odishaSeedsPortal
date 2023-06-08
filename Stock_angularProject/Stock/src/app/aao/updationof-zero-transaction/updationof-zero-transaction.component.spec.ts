import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdationofZeroTransactionComponent } from './updationof-zero-transaction.component';

describe('UpdationofZeroTransactionComponent', () => {
  let component: UpdationofZeroTransactionComponent;
  let fixture: ComponentFixture<UpdationofZeroTransactionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdationofZeroTransactionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdationofZeroTransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
