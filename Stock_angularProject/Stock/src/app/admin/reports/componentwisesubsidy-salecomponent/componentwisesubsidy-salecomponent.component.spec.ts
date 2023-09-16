import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentwisesubsidySalecomponentComponent } from './componentwisesubsidy-salecomponent.component';

describe('ComponentwisesubsidySalecomponentComponent', () => {
  let component: ComponentwisesubsidySalecomponentComponent;
  let fixture: ComponentFixture<ComponentwisesubsidySalecomponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ComponentwisesubsidySalecomponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ComponentwisesubsidySalecomponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
