import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiveDetailsComponent } from './receive-details.component';

describe('ReceiveDetailsComponent', () => {
  let component: ReceiveDetailsComponent;
  let fixture: ComponentFixture<ReceiveDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReceiveDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReceiveDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
