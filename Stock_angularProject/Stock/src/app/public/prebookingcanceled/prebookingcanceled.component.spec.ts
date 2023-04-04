import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrebookingcanceledComponent } from './prebookingcanceled.component';

describe('PrebookingcanceledComponent', () => {
  let component: PrebookingcanceledComponent;
  let fixture: ComponentFixture<PrebookingcanceledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrebookingcanceledComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrebookingcanceledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
