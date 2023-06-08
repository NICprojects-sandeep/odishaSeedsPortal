import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicalSaleEntryComponent } from './physical-sale-entry.component';

describe('PhysicalSaleEntryComponent', () => {
  let component: PhysicalSaleEntryComponent;
  let fixture: ComponentFixture<PhysicalSaleEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhysicalSaleEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicalSaleEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
