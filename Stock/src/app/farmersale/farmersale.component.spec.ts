import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmersaleComponent } from './farmersale.component';

describe('FarmersaleComponent', () => {
  let component: FarmersaleComponent;
  let fixture: ComponentFixture<FarmersaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmersaleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmersaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
