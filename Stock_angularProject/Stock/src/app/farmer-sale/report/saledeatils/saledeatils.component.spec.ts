import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaledeatilsComponent } from './saledeatils.component';

describe('SaledeatilsComponent', () => {
  let component: SaledeatilsComponent;
  let fixture: ComponentFixture<SaledeatilsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SaledeatilsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SaledeatilsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
