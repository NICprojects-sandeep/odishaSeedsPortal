import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeedliveInventoryComponent } from './seedlive-inventory.component';

describe('SeedliveInventoryComponent', () => {
  let component: SeedliveInventoryComponent;
  let fixture: ComponentFixture<SeedliveInventoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeedliveInventoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeedliveInventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
