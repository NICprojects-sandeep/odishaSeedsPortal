import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiaFooterComponent } from './odia-footer.component';

describe('OdiaFooterComponent', () => {
  let component: OdiaFooterComponent;
  let fixture: ComponentFixture<OdiaFooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiaFooterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiaFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
