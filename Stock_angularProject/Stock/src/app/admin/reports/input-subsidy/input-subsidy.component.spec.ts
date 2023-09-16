import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputSubsidyComponent } from './input-subsidy.component';

describe('InputSubsidyComponent', () => {
  let component: InputSubsidyComponent;
  let fixture: ComponentFixture<InputSubsidyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InputSubsidyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InputSubsidyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
