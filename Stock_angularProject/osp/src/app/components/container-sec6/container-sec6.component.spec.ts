import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainerSec6Component } from './container-sec6.component';

describe('ContainerSec6Component', () => {
  let component: ContainerSec6Component;
  let fixture: ComponentFixture<ContainerSec6Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContainerSec6Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContainerSec6Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
