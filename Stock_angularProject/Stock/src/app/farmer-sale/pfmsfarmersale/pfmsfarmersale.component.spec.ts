import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PfmsfarmersaleComponent } from './pfmsfarmersale.component';

describe('PfmsfarmersaleComponent', () => {
  let component: PfmsfarmersaleComponent;
  let fixture: ComponentFixture<PfmsfarmersaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PfmsfarmersaleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PfmsfarmersaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
