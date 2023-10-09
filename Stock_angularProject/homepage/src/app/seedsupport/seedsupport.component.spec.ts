import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeedsupportComponent } from './seedsupport.component';

describe('SeedsupportComponent', () => {
  let component: SeedsupportComponent;
  let fixture: ComponentFixture<SeedsupportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeedsupportComponent]
    });
    fixture = TestBed.createComponent(SeedsupportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
