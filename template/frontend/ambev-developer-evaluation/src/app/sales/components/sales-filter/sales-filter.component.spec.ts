import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { of } from 'rxjs';

import { SalesFilterComponent } from './sales-filter.component';
import { ClientService } from 'src/app/clients/services/clients.service';
import { SubsidiaryService } from '@subsidiaries/services/subsidiary.service';

describe('SalesFilterComponent', () => {
  let component: SalesFilterComponent;
  let fixture: ComponentFixture<SalesFilterComponent>;

  let clientServiceSpy: jasmine.SpyObj<ClientService>;
  let subsidiaryServiceSpy: jasmine.SpyObj<SubsidiaryService>;

  beforeEach(async () => {
    clientServiceSpy = jasmine.createSpyObj('ClientService', [
      'search'
    ]);

    subsidiaryServiceSpy = jasmine.createSpyObj('SubsidiaryService', [
      'search'
    ]);

    await TestBed.configureTestingModule({
      imports: [
        SalesFilterComponent
      ],
      providers: [
        FormBuilder,
        {
          provide: ClientService,
          useValue: clientServiceSpy
        },
        {
          provide: SubsidiaryService,
          useValue: subsidiaryServiceSpy
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SalesFilterComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize filtered observables on init', () => {
    expect(component.filteredClients$).toBeTruthy();
    expect(component.filteredSubsidiaries$).toBeTruthy();
  });

  it('should emit filter when submit is called', () => {
    spyOn(component.filterChanged, 'emit');

    component.filterForm.patchValue({
      number: 10,
      clientId: 'client-id',
      subsidiaryId: 'subsidiary-id'
    } as any);

    component.onSubmit();

    expect(component.filterChanged.emit)
      .toHaveBeenCalledWith({
        number: 10,
        clientId: 'client-id',
        subsidiaryId: 'subsidiary-id',
        pageIndex: 1,
        pageSize: 10
      });
  });

  it('should display client name', () => {
    const client: any = {
      id: '1',
      name: 'John Doe',
      legalName: 'John Company'
    };

    expect(component.displayClient(client))
      .toBe('John Doe');
  });

  it('should display client legal name when name is empty', () => {
    const client: any = {
      id: '1',
      name: '',
      legalName: 'John Company'
    };

    expect(component.displayClient(client))
      .toBe('John Company');
  });

  it('should return empty string when client is null', () => {
    expect(component.displayClient(null as any))
      .toBe('');
  });

  it('should display subsidiary trade name', () => {
    const subsidiary: any = {
      id: '1',
      tradeName: 'Main Store',
      legalName: 'Main Store LTDA'
    };

    expect(component.displaySubsidiary(subsidiary))
      .toBe('Main Store');
  });

  it('should display subsidiary legal name when trade name is empty', () => {
    const subsidiary: any = {
      id: '1',
      tradeName: '',
      legalName: 'Main Store LTDA'
    };

    expect(component.displaySubsidiary(subsidiary))
      .toBe('Main Store LTDA');
  });

  it('should return empty string when subsidiary is null', () => {
    expect(component.displaySubsidiary(null as any))
      .toBe('');
  });

  it('should search clients when typing in autocomplete', fakeAsync(() => {
    const response: any = {
      data: [
        {
          id: '1',
          name: 'John'
        }
      ]
    };

    clientServiceSpy.search.and.returnValue(of(response));

    let result: any;

    component.filteredClients$.subscribe(data => {
      result = data;
    });

    component.clientSearchControl.setValue('John');

    tick(300);

    expect(clientServiceSpy.search)
      .toHaveBeenCalledWith('john');

    expect(result)
      .toEqual(response.data);
  }));

  it('should not search clients when value is empty', fakeAsync(() => {
    component.clientSearchControl.setValue('');

    tick(300);

    expect(clientServiceSpy.search)
      .not.toHaveBeenCalled();
  }));

  it('should set clientId when selecting a client', () => {
    const client: any = {
      id: 'client-id',
      name: 'John'
    };

    component.clientSearchControl.setValue(client);

    expect(component.filterForm.value.clientId)
      .toBe('client-id');
  });

  it('should clear clientId when client input is cleared', () => {
    component.filterForm.patchValue({
      clientId: 'client-id'
    });

    component.clientSearchControl.setValue('');

    expect(component.filterForm.value.clientId)
      .toBe('');
  });

  it('should search subsidiaries when typing in autocomplete', fakeAsync(() => {
    const response: any = {
      data: [
        {
          id: '1',
          tradeName: 'Store'
        }
      ]
    };

    subsidiaryServiceSpy.search.and.returnValue(of(response));

    let result: any;

    component.filteredSubsidiaries$.subscribe(data => {
      result = data;
    });

    component.subsidiarySearchControl.setValue('Store');

    tick(300);

    expect(subsidiaryServiceSpy.search)
      .toHaveBeenCalledWith('store');

    expect(result)
      .toEqual(response.data);
  }));

  it('should set subsidiaryId when selecting a subsidiary', () => {
    const subsidiary: any = {
      id: 'subsidiary-id',
      tradeName: 'Store'
    };

    component.subsidiarySearchControl.setValue(subsidiary);

    expect(component.filterForm.value.subsidiaryId)
      .toBe('subsidiary-id');
  });

  it('should clear subsidiaryId when subsidiary input is cleared', () => {
    component.filterForm.patchValue({
      subsidiaryId: 'subsidiary-id'
    });

    component.subsidiarySearchControl.setValue('');

    expect(component.filterForm.value.subsidiaryId)
      .toBe('');
  });
});