import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

import { SubsidiaryService } from './subsidiary.service';
import { environment } from '@environments/environment';

describe('SubsidiaryService', () => {
  let service: SubsidiaryService;
  let httpMock: HttpTestingController;

  const baseUrl = `${environment.api.baseUrl}/Subsidiary`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        SubsidiaryService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(SubsidiaryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should search subsidiaries by term', () => {
    const term = 'Brazil';

    const mockResponse: any = {
      success: true,
      data: [
        {
          id: '1',
          name: 'Main Subsidiary'
        }
      ],
      message: 'Subsidiaries found'
    };

    service.search(term).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
      expect(response.success).toBeTrue();
      expect(response.data.length).toBe(1);
      expect(response.data[0].name).toBe('Main Subsidiary');
    });

    const req = httpMock.expectOne(
      `${baseUrl}/search?term=${term}`
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should return empty list when no subsidiaries are found', () => {
    const term = 'Unknown';

    const mockResponse: any = {
      success: true,
      data: [],
      message: 'No subsidiaries found'
    };

    service.search(term).subscribe((response: any) => {
      expect(response.success).toBeTrue();
      expect(response.data).toEqual([]);
    });

    const req = httpMock.expectOne(
      `${baseUrl}/search?term=${term}`
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should return error when search fails', () => {
    const term = 'Error';

    service.search(term).subscribe({
      next: () => fail('should have failed'),
      error: (error: any) => {
        expect(error.status).toBe(500);
        expect(error.statusText).toBe('Server Error');
      }
    });

    const req = httpMock.expectOne(
      `${baseUrl}/search?term=${term}`
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
});