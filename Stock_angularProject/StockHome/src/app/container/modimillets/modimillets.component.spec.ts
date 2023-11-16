import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModimilletsComponent } from './modimillets.component';

describe('ModimilletsComponent', () => {
  let component: ModimilletsComponent;
  let fixture: ComponentFixture<ModimilletsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModimilletsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModimilletsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
