import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockreceiveentryComponent } from './stockreceiveentry.component';

describe('StockreceiveentryComponent', () => {
  let component: StockreceiveentryComponent;
  let fixture: ComponentFixture<StockreceiveentryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockreceiveentryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockreceiveentryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
