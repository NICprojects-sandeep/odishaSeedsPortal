import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerstockdtlComponent } from './dealerstockdtl.component';

describe('DealerstockdtlComponent', () => {
  let component: DealerstockdtlComponent;
  let fixture: ComponentFixture<DealerstockdtlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DealerstockdtlComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DealerstockdtlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
