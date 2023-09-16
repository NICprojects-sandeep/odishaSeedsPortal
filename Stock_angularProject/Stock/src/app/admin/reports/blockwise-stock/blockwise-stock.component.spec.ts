import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlockwiseStockComponent } from './blockwise-stock.component';

describe('BlockwiseStockComponent', () => {
  let component: BlockwiseStockComponent;
  let fixture: ComponentFixture<BlockwiseStockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlockwiseStockComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BlockwiseStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
