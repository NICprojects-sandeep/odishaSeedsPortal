import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarietywiseliftComponent } from './varietywiselift.component';

describe('VarietywiseliftComponent', () => {
  let component: VarietywiseliftComponent;
  let fixture: ComponentFixture<VarietywiseliftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VarietywiseliftComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VarietywiseliftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
