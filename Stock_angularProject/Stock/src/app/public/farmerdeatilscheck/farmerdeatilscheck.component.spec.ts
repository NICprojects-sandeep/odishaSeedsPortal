import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmerdeatilscheckComponent } from './farmerdeatilscheck.component';

describe('FarmerdeatilscheckComponent', () => {
  let component: FarmerdeatilscheckComponent;
  let fixture: ComponentFixture<FarmerdeatilscheckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmerdeatilscheckComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmerdeatilscheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
