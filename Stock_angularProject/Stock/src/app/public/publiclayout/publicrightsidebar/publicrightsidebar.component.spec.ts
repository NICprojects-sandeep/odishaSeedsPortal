import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicrightsidebarComponent } from './publicrightsidebar.component';

describe('PublicrightsidebarComponent', () => {
  let component: PublicrightsidebarComponent;
  let fixture: ComponentFixture<PublicrightsidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicrightsidebarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicrightsidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
