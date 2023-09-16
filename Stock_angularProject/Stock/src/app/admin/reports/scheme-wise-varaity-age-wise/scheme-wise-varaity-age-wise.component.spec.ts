import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchemeWiseVaraityAgeWiseComponent } from './scheme-wise-varaity-age-wise.component';

describe('SchemeWiseVaraityAgeWiseComponent', () => {
  let component: SchemeWiseVaraityAgeWiseComponent;
  let fixture: ComponentFixture<SchemeWiseVaraityAgeWiseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchemeWiseVaraityAgeWiseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchemeWiseVaraityAgeWiseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
