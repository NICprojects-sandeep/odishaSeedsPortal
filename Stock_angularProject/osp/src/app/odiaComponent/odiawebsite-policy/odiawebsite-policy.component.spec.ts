import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiawebsitePolicyComponent } from './odiawebsite-policy.component';

describe('OdiawebsitePolicyComponent', () => {
  let component: OdiawebsitePolicyComponent;
  let fixture: ComponentFixture<OdiawebsitePolicyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiawebsitePolicyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiawebsitePolicyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
