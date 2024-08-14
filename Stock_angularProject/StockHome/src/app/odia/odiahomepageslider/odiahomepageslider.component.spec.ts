import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiahomepagesliderComponent } from './odiahomepageslider.component';

describe('OdiahomepagesliderComponent', () => {
  let component: OdiahomepagesliderComponent;
  let fixture: ComponentFixture<OdiahomepagesliderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiahomepagesliderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiahomepagesliderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
