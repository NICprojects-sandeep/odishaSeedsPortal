import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeestructureComponent } from './feestructure.component';

describe('FeestructureComponent', () => {
  let component: FeestructureComponent;
  let fixture: ComponentFixture<FeestructureComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FeestructureComponent]
    });
    fixture = TestBed.createComponent(FeestructureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
