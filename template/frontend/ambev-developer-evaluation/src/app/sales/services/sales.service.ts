import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { CreateUpdateSaleRequest } from '@sales/interfaces/create-sale-request.interface';
import { SaleFilter } from '@sales/interfaces/sale-filter-interface';
import { SaleResponse } from '@sales/interfaces/sale-reponse.interface';
import { ApiOperationResponse } from '@shared/interfaces/api-operation-response.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  private readonly baseUrl = `${environment.api.baseUrl}/Sale`;

  constructor(private readonly http: HttpClient) { }

  getPaginatedByParam(params: SaleFilter) : Observable<any> {
    let httpParams = new HttpParams();
    if(params.number) {
      httpParams = httpParams.set('number', params.number.toString());
    }
    if(params.clientId) {
      httpParams = httpParams.set('clientId', params.clientId);
    }
    if(params.subsidiaryId) {
      httpParams = httpParams.set('subsidiaryId', params.subsidiaryId);
    }
    httpParams = httpParams.set('pageIndex', params.pageIndex.toString());
    httpParams = httpParams.set('pageSize', params.pageSize.toString());
    return this.http.get<any>(`${this.baseUrl}`, { params: httpParams });
  }

  getById(id: string) : Observable<ApiOperationResponse<SaleResponse>> {
    return this.http.get<ApiOperationResponse<SaleResponse>>(`${this.baseUrl}/${id}`);
  }

  create(sale: CreateUpdateSaleRequest) : Observable<ApiOperationResponse<SaleResponse>> {
    return this.http.post<ApiOperationResponse<SaleResponse>>(`${this.baseUrl}`, sale);
  }

  update(id: string, sale: CreateUpdateSaleRequest) : Observable<ApiOperationResponse<SaleResponse>> {
    return this.http.put<ApiOperationResponse<SaleResponse>>(`${this.baseUrl}/${id}`, sale);
  }

  cancel(id: string) : Observable<ApiOperationResponse<SaleResponse>> {
    return this.http.patch<ApiOperationResponse<SaleResponse>>(`${this.baseUrl}/cancel/${id}`, {});
  }

  cancelItem(saleItemId: string) : Observable<ApiOperationResponse<SaleResponse>> {
    return this.http.patch<ApiOperationResponse<SaleResponse>>(`${this.baseUrl}/cancel/item/${saleItemId}`, {});
  }
}
