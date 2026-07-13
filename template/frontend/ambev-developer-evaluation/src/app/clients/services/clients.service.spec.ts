import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

import { ClientService } from './clients.service';
import { environment } from '@environments/environment';
import { ApiOperationResponse } from '@shared/interfaces/api-operation-response.interface';
import { ClientResponse } from '@clients/interfaces/client-response';

describe('ClientService', () => {
  let service: ClientService;
  let httpMock: HttpTestingController;

  const baseUrl = `${environment.api.baseUrl}/Client`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ClientService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(ClientService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should search clients by term', () => {
    const term = 'John';

    const mockResponse: ApiOperationResponse<ClientResponse[]> = {
      success: true,
      data: [
        {
          id: '1',
          name: 'John Doe',
          cpf: '12345678900',
          phoneNumber: '5544332211',
          address: '',
          city: '',
          email: '',
          state: ''
        }
      ],
      message: 'Clients found'
    };

    service.search(term).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
      expect(response.data.length).toBe(1);
      expect(response.data[0].name).toBe('John Doe');
    });

    const req = httpMock.expectOne(
      `${baseUrl}/search?term=${term}`
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should return empty list when no clients are found', () => {
    const term = 'Unknown';

    const mockResponse: ApiOperationResponse<ClientResponse[]> = {
      success: true,
      data: [],
      message: 'No clients found'
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

  it('should propagate http errors', () => {
    const term = 'error';

    service.search(term).subscribe({
      next: () => fail('should have failed'),
      error: (error : any) => {
        expect(error.status).toBe(500);
      }
    });

    const req = httpMock.expectOne(
      `${baseUrl}/search?term=${term}`
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