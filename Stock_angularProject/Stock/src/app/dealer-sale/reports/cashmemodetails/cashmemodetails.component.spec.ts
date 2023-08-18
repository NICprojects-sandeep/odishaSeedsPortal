import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashmemodetailsComponent } from './cashmemodetails.component';

describe('CashmemodetailsComponent', () => {
  let component: CashmemodetailsComponent;
  let fixture: ComponentFixture<CashmemodetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CashmemodetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CashmemodetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
