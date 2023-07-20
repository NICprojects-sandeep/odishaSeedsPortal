import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WebsitePolicyComponent } from './website-policy.component';

describe('WebsitePolicyComponent', () => {
  let component: WebsitePolicyComponent;
  let fixture: ComponentFixture<WebsitePolicyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WebsitePolicyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WebsitePolicyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
