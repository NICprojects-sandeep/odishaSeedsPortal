import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DelerwisesaleComponent } from './delerwisesale.component';

describe('DelerwisesaleComponent', () => {
  let component: DelerwisesaleComponent;
  let fixture: ComponentFixture<DelerwisesaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DelerwisesaleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DelerwisesaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
