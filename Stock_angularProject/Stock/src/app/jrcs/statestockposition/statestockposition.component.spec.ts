import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatestockpositionComponent } from './statestockposition.component';

describe('StatestockpositionComponent', () => {
  let component: StatestockpositionComponent;
  let fixture: ComponentFixture<StatestockpositionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatestockpositionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StatestockpositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
