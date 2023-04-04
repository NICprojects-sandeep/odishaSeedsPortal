import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashmemoTranscationdeletedComponent } from './cashmemo-transcationdeleted.component';

describe('CashmemoTranscationdeletedComponent', () => {
  let component: CashmemoTranscationdeletedComponent;
  let fixture: ComponentFixture<CashmemoTranscationdeletedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CashmemoTranscationdeletedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CashmemoTranscationdeletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
