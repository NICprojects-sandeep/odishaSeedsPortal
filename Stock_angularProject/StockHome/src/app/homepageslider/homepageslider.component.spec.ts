import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomepagesliderComponent } from './homepageslider.component';

describe('HomepagesliderComponent', () => {
  let component: HomepagesliderComponent;
  let fixture: ComponentFixture<HomepagesliderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomepagesliderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomepagesliderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
