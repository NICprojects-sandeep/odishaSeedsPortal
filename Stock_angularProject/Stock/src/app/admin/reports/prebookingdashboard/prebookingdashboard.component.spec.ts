import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrebookingdashboardComponent } from './prebookingdashboard.component';

describe('PrebookingdashboardComponent', () => {
  let component: PrebookingdashboardComponent;
  let fixture: ComponentFixture<PrebookingdashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrebookingdashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrebookingdashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
