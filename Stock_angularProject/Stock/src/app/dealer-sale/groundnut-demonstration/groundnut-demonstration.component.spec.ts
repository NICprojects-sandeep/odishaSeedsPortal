import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroundnutDemonstrationComponent } from './groundnut-demonstration.component';

describe('GroundnutDemonstrationComponent', () => {
  let component: GroundnutDemonstrationComponent;
  let fixture: ComponentFixture<GroundnutDemonstrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroundnutDemonstrationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroundnutDemonstrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
