import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DilougeboxComponent } from './dilougebox.component';

describe('DilougeboxComponent', () => {
  let component: DilougeboxComponent;
  let fixture: ComponentFixture<DilougeboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DilougeboxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DilougeboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
