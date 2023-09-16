import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealerPACSReceiveComponent } from './dealer-pacsreceive.component';

describe('DealerPACSReceiveComponent', () => {
  let component: DealerPACSReceiveComponent;
  let fixture: ComponentFixture<DealerPACSReceiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DealerPACSReceiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DealerPACSReceiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
