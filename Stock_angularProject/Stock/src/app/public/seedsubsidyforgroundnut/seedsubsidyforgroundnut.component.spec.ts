import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeedsubsidyforgroundnutComponent } from './seedsubsidyforgroundnut.component';

describe('SeedsubsidyforgroundnutComponent', () => {
  let component: SeedsubsidyforgroundnutComponent;
  let fixture: ComponentFixture<SeedsubsidyforgroundnutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeedsubsidyforgroundnutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeedsubsidyforgroundnutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
