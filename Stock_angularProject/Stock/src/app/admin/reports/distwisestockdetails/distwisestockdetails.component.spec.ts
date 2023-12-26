import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DistwisestockdetailsComponent } from './distwisestockdetails.component';

describe('DistwisestockdetailsComponent', () => {
  let component: DistwisestockdetailsComponent;
  let fixture: ComponentFixture<DistwisestockdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DistwisestockdetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DistwisestockdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
