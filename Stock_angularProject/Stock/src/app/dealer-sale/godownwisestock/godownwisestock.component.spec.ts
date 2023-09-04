import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GodownwisestockComponent } from './godownwisestock.component';

describe('GodownwisestockComponent', () => {
  let component: GodownwisestockComponent;
  let fixture: ComponentFixture<GodownwisestockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GodownwisestockComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GodownwisestockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
