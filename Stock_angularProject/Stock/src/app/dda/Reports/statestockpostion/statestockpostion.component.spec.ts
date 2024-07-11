import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatestockpostionComponent } from './statestockpostion.component';

describe('StatestockpostionComponent', () => {
  let component: StatestockpostionComponent;
  let fixture: ComponentFixture<StatestockpostionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StatestockpostionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StatestockpostionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
