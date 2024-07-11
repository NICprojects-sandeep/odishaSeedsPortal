import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlockwisetransctionComponent } from './blockwisetransction.component';

describe('BlockwisetransctionComponent', () => {
  let component: BlockwisetransctionComponent;
  let fixture: ComponentFixture<BlockwisetransctionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlockwisetransctionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BlockwisetransctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
