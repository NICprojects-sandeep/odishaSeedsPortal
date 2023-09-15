import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DistWiseCurrentStockComponent } from './dist-wise-current-stock.component';

describe('DistWiseCurrentStockComponent', () => {
  let component: DistWiseCurrentStockComponent;
  let fixture: ComponentFixture<DistWiseCurrentStockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DistWiseCurrentStockComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DistWiseCurrentStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
