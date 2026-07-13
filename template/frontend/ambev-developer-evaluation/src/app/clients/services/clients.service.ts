import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ClientResponse } from '@clients/interfaces/client-response';
import { environment } from '@environments/environment';
import { ApiOperationResponse } from '@shared/interfaces/api-operation-response.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.api.baseUrl}/Client`;

  search(term: string) : Observable<ApiOperationResponse<ClientResponse[]>> {
    return this.http.get<ApiOperationResponse<ClientResponse[]>>(`${this.baseUrl}/search?term=${term}`);
  }
}
