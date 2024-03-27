import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainerSec1Component } from './container-sec1.component';

describe('ContainerSec1Component', () => {
  let component: ContainerSec1Component;
  let fixture: ComponentFixture<ContainerSec1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContainerSec1Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContainerSec1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
