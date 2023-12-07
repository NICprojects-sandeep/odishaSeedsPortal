import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrebookingdetailsComponent } from './prebookingdetails.component';

describe('PrebookingdetailsComponent', () => {
  let component: PrebookingdetailsComponent;
  let fixture: ComponentFixture<PrebookingdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrebookingdetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrebookingdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
