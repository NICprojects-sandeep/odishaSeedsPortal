import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartVarietyListComponent } from './chart-variety-list.component';

describe('ChartVarietyListComponent', () => {
  let component: ChartVarietyListComponent;
  let fixture: ComponentFixture<ChartVarietyListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChartVarietyListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartVarietyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
