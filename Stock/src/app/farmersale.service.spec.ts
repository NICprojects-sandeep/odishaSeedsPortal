import { TestBed } from '@angular/core/testing';

import { FarmersaleService } from './farmersale.service';

describe('FarmersaleService', () => {
  let service: FarmersaleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FarmersaleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
