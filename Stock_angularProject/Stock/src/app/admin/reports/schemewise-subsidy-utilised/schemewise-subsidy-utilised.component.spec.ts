import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchemewiseSubsidyUtilisedComponent } from './schemewise-subsidy-utilised.component';

describe('SchemewiseSubsidyUtilisedComponent', () => {
  let component: SchemewiseSubsidyUtilisedComponent;
  let fixture: ComponentFixture<SchemewiseSubsidyUtilisedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchemewiseSubsidyUtilisedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchemewiseSubsidyUtilisedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
