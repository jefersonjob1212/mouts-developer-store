import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

import { ProductService } from './product.service';
import { environment } from '@environments/environment';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  const baseUrl = `${environment.api.baseUrl}/Product`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ProductService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should search products by name', () => {
    const name = 'Notebook';

    const mockResponse: any = {
      success: true,
      data: [
        {
          id: '1',
          name: 'Notebook Dell'
        }
      ],
      message: 'Products found'
    };

    service.searchByName(name).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
      expect(response.success).toBeTrue();
      expect(response.data.length).toBe(1);
      expect(response.data[0].name).toBe('Notebook Dell');
    });

    const req = httpMock.expectOne(
      `${baseUrl}/by-name/${name}`
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should return empty list when no products are found by name', () => {
    const name = 'Unknown';

    const mockResponse: any = {
      success: true,
      data: [],
      message: 'No products found'
    };

    service.searchByName(name).subscribe((response: any) => {
      expect(response.success).toBeTrue();
      expect(response.data).toEqual([]);
    });

    const req = httpMock.expectOne(
      `${baseUrl}/by-name/${name}`
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should search product by code', () => {
    const code = '12345';

    const mockResponse: any = {
      success: true,
      data: {
        id: '1',
        name: 'Notebook Dell',
        code: '12345'
      },
      message: 'Product found'
    };

    service.searchByCode(code).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
      expect(response.success).toBeTrue();
      expect(response.data.code).toBe(code);
    });

    const req = httpMock.expectOne(
      `${baseUrl}/by-code/${code}`
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should return error when searching products by name fails', () => {
    const name = 'Error';

    service.searchByName(name).subscribe({
      next: (response: any) => fail('should have failed'),
      error: (error: any) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Server Error');
      }
    });

    const req = httpMock.expectOne(
      `${baseUrl}/by-name/${name}`
    );

    expect(req.request.method).toBe('GET');

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

  it('should return error when searching product by code fails', () => {
    const code = '99999';

    service.searchByCode(code).subscribe({
      next: (response: any) => fail('should have failed'),
      error: (error: any) => {
        expect(error.status).toBe(404);
        expect(error.statusText).toBe('Not Found');
      }
    });

    const req = httpMock.expectOne(
      `${baseUrl}/by-code/${code}`
    );

    expect(req.request.method).toBe('GET');

    req.flush(
      {
        message: 'Product not found'
      },
      {
        status: 404,
        statusText: 'Not Found'
      }
    );
  });
});