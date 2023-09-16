import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoundationtoCertifiedLotsComponent } from './foundationto-certified-lots.component';

describe('FoundationtoCertifiedLotsComponent', () => {
  let component: FoundationtoCertifiedLotsComponent;
  let fixture: ComponentFixture<FoundationtoCertifiedLotsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoundationtoCertifiedLotsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FoundationtoCertifiedLotsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
