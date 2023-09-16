import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VarietysearchComponent } from './varietysearch.component';

describe('VarietysearchComponent', () => {
  let component: VarietysearchComponent;
  let fixture: ComponentFixture<VarietysearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VarietysearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VarietysearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
