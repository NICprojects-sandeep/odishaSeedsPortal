import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StocksaleComponent } from './stocksale.component';

describe('StocksaleComponent', () => {
  let component: StocksaleComponent;
  let fixture: ComponentFixture<StocksaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StocksaleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StocksaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
