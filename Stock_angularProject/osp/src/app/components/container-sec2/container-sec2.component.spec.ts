import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainerSec2Component } from './container-sec2.component';

describe('ContainerSec2Component', () => {
  let component: ContainerSec2Component;
  let fixture: ComponentFixture<ContainerSec2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContainerSec2Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContainerSec2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
