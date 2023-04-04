import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainerSec5Component } from './container-sec5.component';

describe('ContainerSec5Component', () => {
  let component: ContainerSec5Component;
  let fixture: ComponentFixture<ContainerSec5Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContainerSec5Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContainerSec5Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
