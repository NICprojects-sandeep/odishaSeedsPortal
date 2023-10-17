import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraphModalComponent } from './graph-modal.component';

describe('GraphModalComponent', () => {
  let component: GraphModalComponent;
  let fixture: ComponentFixture<GraphModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GraphModalComponent]
    });
    fixture = TestBed.createComponent(GraphModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
