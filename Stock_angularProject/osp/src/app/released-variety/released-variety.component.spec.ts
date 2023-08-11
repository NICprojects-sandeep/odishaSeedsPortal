import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReleasedVarietyComponent } from './released-variety.component';

describe('ReleasedVarietyComponent', () => {
  let component: ReleasedVarietyComponent;
  let fixture: ComponentFixture<ReleasedVarietyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReleasedVarietyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReleasedVarietyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
