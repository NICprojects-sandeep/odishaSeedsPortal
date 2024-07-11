import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentstockpostionComponent } from './currentstockpostion.component';

describe('CurrentstockpostionComponent', () => {
  let component: CurrentstockpostionComponent;
  let fixture: ComponentFixture<CurrentstockpostionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrentstockpostionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CurrentstockpostionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
