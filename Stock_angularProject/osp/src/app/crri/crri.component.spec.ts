import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrriComponent } from './crri.component';

describe('CrriComponent', () => {
  let component: CrriComponent;
  let fixture: ComponentFixture<CrriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CrriComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CrriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
