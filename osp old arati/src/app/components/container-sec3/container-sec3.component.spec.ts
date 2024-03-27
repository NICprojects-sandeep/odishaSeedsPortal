import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainerSec3Component } from './container-sec3.component';

describe('ContainerSec3Component', () => {
  let component: ContainerSec3Component;
  let fixture: ComponentFixture<ContainerSec3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContainerSec3Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContainerSec3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
