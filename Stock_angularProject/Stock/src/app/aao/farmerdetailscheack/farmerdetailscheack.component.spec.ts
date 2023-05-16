import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerdetailscheackComponent } from './farmerdetailscheack.component';

describe('FarmerdetailscheackComponent', () => {
  let component: FarmerdetailscheackComponent;
  let fixture: ComponentFixture<FarmerdetailscheackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerdetailscheackComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmerdetailscheackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
