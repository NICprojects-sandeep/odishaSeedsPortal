import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerwisesaledetailsComponent } from './dealerwisesaledetails.component';

describe('DealerwisesaledetailsComponent', () => {
  let component: DealerwisesaledetailsComponent;
  let fixture: ComponentFixture<DealerwisesaledetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DealerwisesaledetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DealerwisesaledetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
