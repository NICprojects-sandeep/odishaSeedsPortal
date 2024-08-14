import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaseedsupportComponent } from './odiaseedsupport.component';

describe('OdiaseedsupportComponent', () => {
  let component: OdiaseedsupportComponent;
  let fixture: ComponentFixture<OdiaseedsupportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaseedsupportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaseedsupportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
