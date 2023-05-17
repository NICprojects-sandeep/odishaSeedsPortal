import { TestBed } from '@angular/core/testing';

import { AaoService } from './aao.service';

describe('AaoService', () => {
  let service: AaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AaoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
