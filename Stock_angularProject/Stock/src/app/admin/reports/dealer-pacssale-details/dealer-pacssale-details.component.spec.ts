import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerPACSSaleDetailsComponent } from './dealer-pacssale-details.component';

describe('DealerPACSSaleDetailsComponent', () => {
  let component: DealerPACSSaleDetailsComponent;
  let fixture: ComponentFixture<DealerPACSSaleDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DealerPACSSaleDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DealerPACSSaleDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
