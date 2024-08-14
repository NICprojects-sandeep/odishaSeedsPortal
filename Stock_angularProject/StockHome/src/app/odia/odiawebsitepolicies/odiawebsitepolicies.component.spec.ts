import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiawebsitepoliciesComponent } from './odiawebsitepolicies.component';

describe('OdiawebsitepoliciesComponent', () => {
  let component: OdiawebsitepoliciesComponent;
  let fixture: ComponentFixture<OdiawebsitepoliciesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiawebsitepoliciesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiawebsitepoliciesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
