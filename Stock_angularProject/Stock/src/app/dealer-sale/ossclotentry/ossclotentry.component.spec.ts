import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OssclotentryComponent } from './ossclotentry.component';

describe('OssclotentryComponent', () => {
  let component: OssclotentryComponent;
  let fixture: ComponentFixture<OssclotentryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OssclotentryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OssclotentryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
