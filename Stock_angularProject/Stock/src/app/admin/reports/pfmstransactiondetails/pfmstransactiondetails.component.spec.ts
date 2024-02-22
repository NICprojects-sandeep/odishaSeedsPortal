import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PfmstransactiondetailsComponent } from './pfmstransactiondetails.component';

describe('PfmstransactiondetailsComponent', () => {
  let component: PfmstransactiondetailsComponent;
  let fixture: ComponentFixture<PfmstransactiondetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PfmstransactiondetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PfmstransactiondetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
