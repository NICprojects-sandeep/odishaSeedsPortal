import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainerSec4Component } from './container-sec4.component';

describe('ContainerSec4Component', () => {
  let component: ContainerSec4Component;
  let fixture: ComponentFixture<ContainerSec4Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContainerSec4Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContainerSec4Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
