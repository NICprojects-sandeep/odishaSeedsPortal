import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlockwisestockdetailsComponent } from './blockwisestockdetails.component';

describe('BlockwisestockdetailsComponent', () => {
  let component: BlockwisestockdetailsComponent;
  let fixture: ComponentFixture<BlockwisestockdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlockwisestockdetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BlockwisestockdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
