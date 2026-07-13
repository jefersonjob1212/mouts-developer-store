import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

import { SalesService } from './sales.service';
import { environment } from '@environments/environment';

describe('SalesService', () => {
  let service: SalesService;
  let httpMock: HttpTestingController;

  const baseUrl = `${environment.api.baseUrl}/Sale`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        SalesService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(SalesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get paginated sales with filters', () => {
    const params = {
      number: 10,
      clientId: 'client-id',
      subsidiaryId: 'subsidiary-id',
      pageIndex: 0,
      pageSize: 10
    };

    const mockResponse: any = {
      success: true,
      data: [],
      message: 'Sales found'
    };

    service.getPaginatedByParam(params).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(request =>
      request.url === baseUrl &&
      request.params.get('number') === '10' &&
      request.params.get('clientId') === 'client-id' &&
      request.params.get('subsidiaryId') === 'subsidiary-id' &&
      request.params.get('pageIndex') === '0' &&
      request.params.get('pageSize') === '10'
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should get paginated sales without optional filters', () => {
    const params = {
      pageIndex: 0,
      pageSize: 10
    };

    const mockResponse: any = {
      success: true,
      data: [],
      message: 'Sales found'
    };

    service.getPaginatedByParam(params).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(request =>
      request.url === baseUrl &&
      request.params.get('pageIndex') === '0' &&
      request.params.get('pageSize') === '10'
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should get sale by id', () => {
    const id = 'sale-id';

    const mockResponse: any = {
      success: true,
      data: {
        id,
        number: 1
      },
      message: 'Sale found'
    };

    service.getById(id).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
      expect(response.data.id).toBe(id);
    });

    const req = httpMock.expectOne(
      `${baseUrl}/${id}`
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should create a sale', () => {
    const saleRequest: any = {
      number: 1,
      clientId: 'client-id',
      subsidiaryId: 'subsidiary-id',
      items: []
    };

    const mockResponse: any = {
      success: true,
      data: {
        id: 'sale-id'
      },
      message: 'Sale created'
    };

    service.create(saleRequest).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
      expect(response.data.id).toBe('sale-id');
    });

    const req = httpMock.expectOne(baseUrl);

    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(saleRequest);

    req.flush(mockResponse);
  });

  it('should update a sale', () => {
    const id = 'sale-id';

    const saleRequest: any = {
      number: 1,
      clientId: 'client-id',
      subsidiaryId: 'subsidiary-id',
      items: []
    };

    const mockResponse: any = {
      success: true,
      data: {
        id
      },
      message: 'Sale updated'
    };

    service.update(id, saleRequest).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(
      `${baseUrl}/${id}`
    );

    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(saleRequest);

    req.flush(mockResponse);
  });

  it('should cancel a sale', () => {
    const id = 'sale-id';

    const mockResponse: any = {
      success: true,
      data: {
        id,
        status: 'Canceled'
      },
      message: 'Sale canceled'
    };

    service.cancel(id).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
      expect(response.data.status).toBe('Canceled');
    });

    const req = httpMock.expectOne(
      `${baseUrl}/cancel/${id}`
    );

    expect(req.request.method).toBe('PATCH');
    expect(req.request.body).toEqual({});

    req.flush(mockResponse);
  });

  it('should cancel sale item', () => {
    const saleItemId = 'item-id';

    const mockResponse: any = {
      success: true,
      data: {
        id: 'sale-id'
      },
      message: 'Sale item canceled'
    };

    service.cancelItem(saleItemId).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(
      `${baseUrl}/cancel/item/${saleItemId}`
    );

    expect(req.request.method).toBe('PATCH');
    expect(req.request.body).toEqual({});

    req.flush(mockResponse);
  });

  it('should return error when request fails', () => {
    const id = 'sale-id';

    service.getById(id).subscribe({
      next: (response: any) => fail('should have failed'),
      error: (error: any) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Server Error');
      }
    });

    const req = httpMock.expectOne(
      `${baseUrl}/${id}`
    );

    req.flush(
      {
        message: 'Internal server error'
      },
      {
        status: 500,
        statusText: 'Server Error'
      }
    );
  });
});