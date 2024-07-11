import { TestBed } from '@angular/core/testing';

import { DdaService } from './dda.service';

describe('DdaService', () => {
  let service: DdaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DdaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
