import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OdiacontainerComponent } from './odiacontainer.component';

describe('OdiacontainerComponent', () => {
  let component: OdiacontainerComponent;
  let fixture: ComponentFixture<OdiacontainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OdiacontainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OdiacontainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
