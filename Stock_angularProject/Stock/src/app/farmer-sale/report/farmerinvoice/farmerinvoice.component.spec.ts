import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerinvoiceComponent } from './farmerinvoice.component';

describe('FarmerinvoiceComponent', () => {
  let component: FarmerinvoiceComponent;
  let fixture: ComponentFixture<FarmerinvoiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerinvoiceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmerinvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
