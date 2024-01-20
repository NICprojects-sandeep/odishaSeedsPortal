import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchemewisedetailsComponent } from './schemewisedetails.component';

describe('SchemewisedetailsComponent', () => {
  let component: SchemewisedetailsComponent;
  let fixture: ComponentFixture<SchemewisedetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchemewisedetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchemewisedetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
