import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicleftsidebarComponent } from './publicleftsidebar.component';

describe('PublicleftsidebarComponent', () => {
  let component: PublicleftsidebarComponent;
  let fixture: ComponentFixture<PublicleftsidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicleftsidebarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicleftsidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
