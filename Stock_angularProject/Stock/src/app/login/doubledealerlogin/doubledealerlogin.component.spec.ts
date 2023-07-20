import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoubledealerloginComponent } from './doubledealerlogin.component';

describe('DoubledealerloginComponent', () => {
  let component: DoubledealerloginComponent;
  let fixture: ComponentFixture<DoubledealerloginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoubledealerloginComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DoubledealerloginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
