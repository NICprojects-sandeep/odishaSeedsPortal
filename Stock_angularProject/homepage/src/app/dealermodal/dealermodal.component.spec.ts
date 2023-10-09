import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealermodalComponent } from './dealermodal.component';

describe('DealermodalComponent', () => {
  let component: DealermodalComponent;
  let fixture: ComponentFixture<DealermodalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DealermodalComponent]
    });
    fixture = TestBed.createComponent(DealermodalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
