import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErupifarmersaleComponent } from './erupifarmersale.component';

describe('ErupifarmersaleComponent', () => {
  let component: ErupifarmersaleComponent;
  let fixture: ComponentFixture<ErupifarmersaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ErupifarmersaleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ErupifarmersaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
