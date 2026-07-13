import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

import { SalesFormComponent } from './sales-form.component';
import { ProductService } from '@products/services/product.service';
import { ClientService } from '@clients/services/clients.service';
import { SubsidiaryService } from '@subsidiaries/services/subsidiary.service';
import { SalesService } from '@sales/services/sales.service';

describe('SalesFormComponent', () => {

  let component: SalesFormComponent;
  let fixture: ComponentFixture<SalesFormComponent>;

  let productServiceSpy: jasmine.SpyObj<ProductService>;
  let clientServiceSpy: jasmine.SpyObj<ClientService>;
  let subsidiaryServiceSpy: jasmine.SpyObj<SubsidiaryService>;
  let salesServiceSpy: jasmine.SpyObj<SalesService>;

  let routerSpy: jasmine.SpyObj<Router>;
  let snackSpy: jasmine.SpyObj<MatSnackBar>;

  beforeEach(async () => {

    productServiceSpy = jasmine.createSpyObj('ProductService', [
      'searchByName',
      'searchByCode'
    ]);

    clientServiceSpy = jasmine.createSpyObj('ClientService', [
      'search'
    ]);

    subsidiaryServiceSpy = jasmine.createSpyObj('SubsidiaryService', [
      'search'
    ]);

    salesServiceSpy = jasmine.createSpyObj('SalesService', [
      'create',
      'update',
      'getById'
    ]);

    routerSpy = jasmine.createSpyObj('Router', [
      'navigate'
    ]);

    snackSpy = jasmine.createSpyObj('MatSnackBar', [
      'open'
    ]);

    await TestBed.configureTestingModule({
      imports: [
        SalesFormComponent
      ],
      providers: [
        FormBuilder,

        {
          provide: ProductService,
          useValue: productServiceSpy
        },
        {
          provide: ClientService,
          useValue: clientServiceSpy
        },
        {
          provide: SubsidiaryService,
          useValue: subsidiaryServiceSpy
        },
        {
          provide: SalesService,
          useValue: salesServiceSpy
        },
        {
          provide: Router,
          useValue: routerSpy
        },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: () => null
              }
            }
          }
        },
        {
          provide: MatSnackBar,
          useValue: snackSpy
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SalesFormComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
    component.ngOnInit();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('should build sale form on init', () => {
    expect(component.saleForm)
      .toBeTruthy();
    expect(component.items.length)
      .toBe(1);
  });


  it('should add item', () => {
    const before = component.items.length;
    component.addItem();
    expect(component.items.length)
      .toBe(before + 1);

  });


  it('should remove item', () => {
    component.removeItem(0);
    expect(component.items.length)
      .toBe(0);
  });


  it('should calculate discount 10 percent when quantity between 4 and 9', () => {
    const item = component.items.at(0);
    item.patchValue({
      unitPrice: 100,
      quantity: 5
    });
    component.calculate(0);

    expect(item.getRawValue().discount)
      .toBe(50);

    expect(item.getRawValue().total)
      .toBe(450);
  });


  it('should calculate discount 20 percent when quantity between 10 and 20', () => {
    const item = component.items.at(0);
    item.patchValue({
      unitPrice: 100,
      quantity: 10
    });
    component.calculate(0);

    expect(item.getRawValue().discount)
      .toBe(200);

    expect(item.getRawValue().total)
      .toBe(800);
  });


  it('should add error when quantity greater than 20', () => {
    const item = component.items.at(0);
    item.patchValue({
      quantity: 21
    });

    component.calculate(0);

    expect(
      item.get('quantity')?.hasError('maxQuantity')
    )
      .toBeTrue();
  });

  it('should calculate total sale', () => {
    component.items.at(0).patchValue({
      total: 100
    });
    component.addItem();
    component.items.at(1).patchValue({
      total: 200
    });

    expect(component.totalSale)
      .toBe(300);
  });


  it('should calculate total discount', () => {
    component.items.at(0).patchValue({
      discount: 50
    });
    expect(component.totalDiscount)
      .toBe(50);
  });

  it('should select product', () => {
    const product: any = {
      id: '1',
      code: '001',
      name: 'Product',
      price: 100
    };

    component.onProductSelected(0, product);

    expect(
      component.items.at(0).get('productId')?.value
    )
      .toBe('1');
  });

  it('should not select duplicated product', () => {
    const product: any = {
      id: '1',
      price: 100
    };

    component.onProductSelected(0, product);
    component.addItem();
    component.onProductSelected(1, product);
    expect(
      component.items.at(1).get('productId')?.value
    )
      .toBeNull();
  });


  it('should display client name', () => {
    expect(
      component.displayClient({
        name: 'John'
      } as any)
    )
      .toBe('John');
  });


  it('should display subsidiary trade name', () => {
    expect(
      component.displaySubsidiary({
        tradeName: 'Store'
      } as any)
    )
      .toBe('Store');
  });


  it('should save new sale', () => {

    salesServiceSpy.create.and.returnValue(
      of({
        data: {}
      } as any)
    );

    component.saleForm.patchValue({
      number: 1,
      clientId: 'client-id',
      subsidiaryId: 'subsidiary-id'
    });

    component.items.at(0).patchValue({
      productId: 'product-id',
      quantity: 1
    });

    component.save();

    expect(salesServiceSpy.create)
      .toHaveBeenCalled();

    expect(routerSpy.navigate)
      .toHaveBeenCalledWith(['']);

  });

  it('should not save invalid form', () => {
    spyOn(component.saleForm, 'markAllAsTouched');
    component.save();
    expect(
      component.saleForm.markAllAsTouched
    )
      .toHaveBeenCalled();
  });
});