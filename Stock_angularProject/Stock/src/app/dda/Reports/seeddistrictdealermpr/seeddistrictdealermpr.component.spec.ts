import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeeddistrictdealermprComponent } from './seeddistrictdealermpr.component';

describe('SeeddistrictdealermprComponent', () => {
  let component: SeeddistrictdealermprComponent;
  let fixture: ComponentFixture<SeeddistrictdealermprComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeeddistrictdealermprComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeeddistrictdealermprComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
