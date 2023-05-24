import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerTypeUpdateComponent } from './farmer-type-update.component';

describe('FarmerTypeUpdateComponent', () => {
  let component: FarmerTypeUpdateComponent;
  let fixture: ComponentFixture<FarmerTypeUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerTypeUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmerTypeUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
