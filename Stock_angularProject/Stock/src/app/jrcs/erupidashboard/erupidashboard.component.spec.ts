import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErupidashboardComponent } from './erupidashboard.component';

describe('ErupidashboardComponent', () => {
  let component: ErupidashboardComponent;
  let fixture: ComponentFixture<ErupidashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ErupidashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ErupidashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
