import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentstockpositionComponent } from './currentstockposition.component';

describe('CurrentstockpositionComponent', () => {
  let component: CurrentstockpositionComponent;
  let fixture: ComponentFixture<CurrentstockpositionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrentstockpositionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CurrentstockpositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
