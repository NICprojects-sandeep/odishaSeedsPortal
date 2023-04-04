import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncentiveoilseedgroundnutComponent } from './incentiveoilseedgroundnut.component';

describe('IncentiveoilseedgroundnutComponent', () => {
  let component: IncentiveoilseedgroundnutComponent;
  let fixture: ComponentFixture<IncentiveoilseedgroundnutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncentiveoilseedgroundnutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IncentiveoilseedgroundnutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
