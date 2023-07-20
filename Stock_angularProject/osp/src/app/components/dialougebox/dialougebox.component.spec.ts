import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialougeboxComponent } from './dialougebox.component';

describe('DialougeboxComponent', () => {
  let component: DialougeboxComponent;
  let fixture: ComponentFixture<DialougeboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialougeboxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialougeboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
