import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { ProductResponse } from '@products/interfaces/product-response';
import { ApiOperationResponse } from '@shared/interfaces/api-operation-response.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.api.baseUrl}/Product`;

  searchByName(name: string): Observable<ApiOperationResponse<ProductResponse[]>> {
    return this.http.get<ApiOperationResponse<ProductResponse[]>>(`${this.baseUrl}/by-name/${name}`);;
  }

  searchByCode(code: string): Observable<ApiOperationResponse<ProductResponse>> {
    return this.http.get<ApiOperationResponse<ProductResponse>>(`${this.baseUrl}/by-code/${code}`);;
  }
}
