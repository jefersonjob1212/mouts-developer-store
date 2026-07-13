import { AsyncPipe } from '@angular/common';
import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ClientResponse } from '@clients/interfaces/client-response';
import { SaleFilter } from '@sales/interfaces/sale-filter-interface';
import { ApiOperationResponse } from '@shared/interfaces/api-operation-response.interface';
import { SubsidiaryResponse } from '@subsidiaries/interfaces/subsidiary-response';
import { SubsidiaryService } from '@subsidiaries/services/subsidiary.service';
import { debounceTime, distinctUntilChanged, map, Observable, of, startWith, switchMap } from 'rxjs';
import { ClientService } from 'src/app/clients/services/clients.service';

@Component({
  selector: 'app-sales-filter',
  imports: [
    FormsModule, 
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    AsyncPipe
  ],
  templateUrl: './sales-filter.component.html',
  styleUrl: './sales-filter.component.scss'
})
export class SalesFilterComponent implements OnInit {

  @Output() filterChanged = new EventEmitter<SaleFilter>();

  private clientsService = inject(ClientService);
  private subsidiariesService = inject(SubsidiaryService);
  private formBuilder = inject(FormBuilder);

  clientSearchControl = new FormControl<string | ClientResponse>('');
  subsidiarySearchControl = new FormControl<string | ClientResponse>('');
  
  filteredClients$!: Observable<ClientResponse[]>;
  filteredSubsidiaries$!: Observable<SubsidiaryResponse[]>;
  filterForm = this.formBuilder.group({
    number: [null],
    clientId: [''],
    subsidiaryId: [''],
    status: ['']
  });

  ngOnInit(): void {
    this.setFilteredClients();
    this.setFilteredSubsidiaries();
  }

  onSubmit(): void {
    const filter: SaleFilter = {
      number: this.filterForm.value.number,
      clientId: this.filterForm.value.clientId,
      subsidiaryId: this.filterForm.value.subsidiaryId,
      pageIndex: 1,
      pageSize: 10
    };
    this.filterChanged.emit(filter);
  }

  displayClient(client: ClientResponse): string {
    if(client) {
      return client.name ? client.name : client.legalName ? client.legalName : '';
    }
    return '';
  }

  displaySubsidiary(subsidiary: SubsidiaryResponse): string {
    console.log('displaySubsidiary called with:', subsidiary);
    if(subsidiary) {
      return subsidiary.tradeName ? subsidiary.tradeName : subsidiary.legalName ? subsidiary.legalName : '';
    }
    return '';
  }

  private setFilteredClients(): void {
    this.filteredClients$ = this.clientSearchControl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(value => {
        const name = typeof value === 'string' ? value : value?.name ? value.name : value?.legalName ? value.legalName : '';
        if(name!.length == 0) {
          return of([]);
        }
        const filterValue = name ? name.toLowerCase() : '';
        return this.clientsService.search(filterValue).pipe(
          map((response: ApiOperationResponse<ClientResponse[]>) => response.data)
        );
      })
    );


    this.clientSearchControl.valueChanges.subscribe(value => {
      if (typeof value === 'object' && value !== null) {
        this.filterForm.patchValue({ clientId: value.id });
      } else {
        this.filterForm.patchValue({ clientId: '' });
      }
    });
  }

  private setFilteredSubsidiaries(): void {
    this.filteredSubsidiaries$ = this.subsidiarySearchControl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(value => {
        const name = typeof value === 'string' ? value : value?.tradeName ? value.tradeName : value?.legalName ? value.legalName : '';
        if(name!.length == 0) {
          return of([]);
        }
        const filterValue = name ? name.toLowerCase() : '';
        return this.subsidiariesService.search(filterValue).pipe(
          map((response: ApiOperationResponse<SubsidiaryResponse[]>) => response.data)
        );
      }
    ));

    this.subsidiarySearchControl.valueChanges.subscribe(value => {
      if (typeof value === 'object' && value !== null) {
        this.filterForm.patchValue({ subsidiaryId: value.id });
      } else {
        this.filterForm.patchValue({ subsidiaryId: '' });
      }
    });
  }

}
