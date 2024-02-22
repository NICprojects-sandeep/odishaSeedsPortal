import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncentiveoilseedgroundnutouatComponent } from './incentiveoilseedgroundnutouat.component';

describe('IncentiveoilseedgroundnutouatComponent', () => {
  let component: IncentiveoilseedgroundnutouatComponent;
  let fixture: ComponentFixture<IncentiveoilseedgroundnutouatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncentiveoilseedgroundnutouatComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IncentiveoilseedgroundnutouatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
