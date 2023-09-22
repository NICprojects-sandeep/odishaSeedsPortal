import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuOdiaComponent } from './menu-odia.component';

describe('MenuOdiaComponent', () => {
  let component: MenuOdiaComponent;
  let fixture: ComponentFixture<MenuOdiaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MenuOdiaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MenuOdiaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
