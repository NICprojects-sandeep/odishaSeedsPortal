import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NearestdealerlistComponent } from './nearestdealerlist.component';

describe('NearestdealerlistComponent', () => {
  let component: NearestdealerlistComponent;
  let fixture: ComponentFixture<NearestdealerlistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NearestdealerlistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NearestdealerlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
