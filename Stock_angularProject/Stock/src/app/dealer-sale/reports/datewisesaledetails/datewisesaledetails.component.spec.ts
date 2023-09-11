import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatewisesaledetailsComponent } from './datewisesaledetails.component';

describe('DatewisesaledetailsComponent', () => {
  let component: DatewisesaledetailsComponent;
  let fixture: ComponentFixture<DatewisesaledetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatewisesaledetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatewisesaledetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
