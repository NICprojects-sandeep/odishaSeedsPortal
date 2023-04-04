import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartLicenceListComponent } from './chart-licence-list.component';

describe('ChartLicenceListComponent', () => {
  let component: ChartLicenceListComponent;
  let fixture: ComponentFixture<ChartLicenceListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChartLicenceListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartLicenceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
