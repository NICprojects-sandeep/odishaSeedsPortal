import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WebsitepoliciesComponent } from './websitepolicies.component';

describe('WebsitepoliciesComponent', () => {
  let component: WebsitepoliciesComponent;
  let fixture: ComponentFixture<WebsitepoliciesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WebsitepoliciesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WebsitepoliciesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
