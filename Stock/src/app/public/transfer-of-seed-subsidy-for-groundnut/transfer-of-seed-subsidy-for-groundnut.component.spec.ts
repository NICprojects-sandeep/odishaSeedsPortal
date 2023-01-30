import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferOfSeedSubsidyForGroundnutComponent } from './transfer-of-seed-subsidy-for-groundnut.component';

describe('TransferOfSeedSubsidyForGroundnutComponent', () => {
  let component: TransferOfSeedSubsidyForGroundnutComponent;
  let fixture: ComponentFixture<TransferOfSeedSubsidyForGroundnutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransferOfSeedSubsidyForGroundnutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransferOfSeedSubsidyForGroundnutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
