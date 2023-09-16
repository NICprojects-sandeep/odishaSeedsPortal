import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DistwisePACSDetailsComponent } from './distwise-pacsdetails.component';

describe('DistwisePACSDetailsComponent', () => {
  let component: DistwisePACSDetailsComponent;
  let fixture: ComponentFixture<DistwisePACSDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DistwisePACSDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DistwisePACSDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
