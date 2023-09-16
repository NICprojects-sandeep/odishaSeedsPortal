import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DistWiseCropAgeWiseSeedDataComponent } from './dist-wise-crop-age-wise-seed-data.component';

describe('DistWiseCropAgeWiseSeedDataComponent', () => {
  let component: DistWiseCropAgeWiseSeedDataComponent;
  let fixture: ComponentFixture<DistWiseCropAgeWiseSeedDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DistWiseCropAgeWiseSeedDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DistWiseCropAgeWiseSeedDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
