import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashmemoreportsComponent } from './cashmemoreports.component';

describe('CashmemoreportsComponent', () => {
  let component: CashmemoreportsComponent;
  let fixture: ComponentFixture<CashmemoreportsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CashmemoreportsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CashmemoreportsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
