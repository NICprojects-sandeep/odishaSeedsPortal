import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceivedetailsComponent } from './receivedetails.component';

describe('ReceivedetailsComponent', () => {
  let component: ReceivedetailsComponent;
  let fixture: ComponentFixture<ReceivedetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReceivedetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReceivedetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
