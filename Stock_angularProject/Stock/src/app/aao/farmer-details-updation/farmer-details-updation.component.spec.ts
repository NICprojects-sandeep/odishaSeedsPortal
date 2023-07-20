import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerDetailsUpdationComponent } from './farmer-details-updation.component';

describe('FarmerDetailsUpdationComponent', () => {
  let component: FarmerDetailsUpdationComponent;
  let fixture: ComponentFixture<FarmerDetailsUpdationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerDetailsUpdationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmerDetailsUpdationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
