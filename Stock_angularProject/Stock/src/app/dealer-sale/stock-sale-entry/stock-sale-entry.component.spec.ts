import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockSaleEntryComponent } from './stock-sale-entry.component';

describe('StockSaleEntryComponent', () => {
  let component: StockSaleEntryComponent;
  let fixture: ComponentFixture<StockSaleEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockSaleEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockSaleEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
