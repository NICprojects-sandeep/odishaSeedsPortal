import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CumulativetransactiondatewiseComponent } from './cumulativetransactiondatewise.component';

describe('CumulativetransactiondatewiseComponent', () => {
  let component: CumulativetransactiondatewiseComponent;
  let fixture: ComponentFixture<CumulativetransactiondatewiseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CumulativetransactiondatewiseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CumulativetransactiondatewiseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
