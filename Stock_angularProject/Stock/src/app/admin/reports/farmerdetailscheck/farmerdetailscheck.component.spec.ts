import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerdetailscheckComponent } from './farmerdetailscheck.component';

describe('FarmerdetailscheckComponent', () => {
  let component: FarmerdetailscheckComponent;
  let fixture: ComponentFixture<FarmerdetailscheckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerdetailscheckComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmerdetailscheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
