import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { ApiOperationResponse } from '@shared/interfaces/api-operation-response.interface';
import { SubsidiaryResponse } from '@subsidiaries/interfaces/subsidiary-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubsidiaryService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.api.baseUrl}/Subsidiary`;

  search(term: string) : Observable<ApiOperationResponse<SubsidiaryResponse[]>> {
    return this.http.get<ApiOperationResponse<SubsidiaryResponse[]>>(`${this.baseUrl}/search?term=${term}`);
  }
}
