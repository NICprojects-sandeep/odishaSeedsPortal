import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PfmsstatusComponent } from './pfmsstatus.component';

describe('PfmsstatusComponent', () => {
  let component: PfmsstatusComponent;
  let fixture: ComponentFixture<PfmsstatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PfmsstatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PfmsstatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
