import { TestBed } from '@angular/core/testing';

import { ConvertReceiptService } from './convert-receipt.service';

describe('ConvertReceiptService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConvertReceiptService = TestBed.get(ConvertReceiptService);
    expect(service).toBeTruthy();
  });
});
