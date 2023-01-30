import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TranscationdetailsdeletedComponent } from './transcationdetailsdeleted.component';

describe('TranscationdetailsdeletedComponent', () => {
  let component: TranscationdetailsdeletedComponent;
  let fixture: ComponentFixture<TranscationdetailsdeletedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TranscationdetailsdeletedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TranscationdetailsdeletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
