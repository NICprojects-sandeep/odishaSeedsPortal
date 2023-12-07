import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateAccDtlsComponent } from './update-acc-dtls.component';

describe('UpdateAccDtlsComponent', () => {
  let component: UpdateAccDtlsComponent;
  let fixture: ComponentFixture<UpdateAccDtlsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateAccDtlsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateAccDtlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
