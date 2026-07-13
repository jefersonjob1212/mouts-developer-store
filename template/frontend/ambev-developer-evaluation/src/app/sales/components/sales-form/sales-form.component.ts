import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { SaleItemCreateComponent } from "../sale-item-create/sale-item-create.component";
import { debounceTime, distinctUntilChanged, map, Observable, of, startWith, switchMap } from 'rxjs';
import { SubsidiaryResponse } from '@subsidiaries/interfaces/subsidiary-response';
import { ClientResponse } from '@clients/interfaces/client-response';
import { ProductResponse } from '@products/interfaces/product-response';
import { SalesService } from '@sales/services/sales.service';
import { SubsidiaryService } from '@subsidiaries/services/subsidiary.service';
import { ClientService } from '@clients/services/clients.service';
import { ProductService } from '@products/services/product.service';
import { ApiOperationResponse } from '@shared/interfaces/api-operation-response.interface';
import { SaleItemViewModel } from '@sales/view-models/sale-item.view-model';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SaleResponse } from '@sales/interfaces/sale-reponse.interface';

type ClientValueForm = ClientResponse | string | null;
type SubsidiaryValueForm = SubsidiaryResponse | string | null;

@Component({
  selector: 'app-sales-form',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatAutocompleteModule,
    MatIconModule,
    CommonModule,
    SaleItemCreateComponent
  ],
  templateUrl: './sales-form.component.html',
  styleUrl: './sales-form.component.scss'
})
export class SalesFormComponent {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private clientService = inject(ClientService);
  private subsidiaryService = inject(SubsidiaryService);
  private saleService = inject(SalesService);
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);
  private snackBar = inject(MatSnackBar);

  saleForm!: FormGroup;

  filteredClients$!: Observable<ClientResponse[]>;
  filteredSubsidiaries$!: Observable<SubsidiaryResponse[]>;

  clientSearchControl = new FormControl<ClientValueForm>('');
  subsidiarySearchControl = new FormControl<SubsidiaryValueForm>('');
  saleItems: SaleItemViewModel[] = [];

  filteredProducts: Observable<ProductResponse[]>[] = [];

  saleId?: string;

  isEdit = false;

  ngOnInit(): void {
    this.buildForm();
    this.configureAutocomplete();
    this.addItem();

    this.saleId = this.activatedRoute.snapshot.paramMap.get('id')!;

    this.isEdit = !!this.saleId;

    if (this.isEdit) {
      this.loadSale();
    }
  }

  private buildForm(): void {
    this.saleForm = this.fb.group({
      number: ['', Validators.required],
      clientId: [null, Validators.required],
      subsidiaryId: [null, Validators.required],
      items: this.fb.array([])
    });
  }

  get items(): FormArray {
    return this.saleForm.get('items') as FormArray;
  }

  private createItem(): FormGroup {
    return this.fb.group({
      productId: [null, Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [{ value: 0, disabled: true }],
      discount: [{ value: 0, disabled: true }],
      total: [{ value: 0, disabled: true }]
    });
  }

  backHome() {
    this.router.navigate(['']);
  }

  addItem(): void {
    const group = this.createItem();

    const productSearchControl = new FormControl<ProductResponse | string | null>('', {
      nonNullable: true
    });

    const filteredProducts$ = productSearchControl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(value => {

        if (!value) {
          return of([]);
        }

        if (typeof value !== 'string') {
          return of([]);
        }

        const term = value.trim();

        const isCode = /^\d+$/.test(term);

        if (isCode) {
          return this.productService.searchByCode(term).pipe(
            map(response =>
              response.data ? [response.data] : []
            )
          );
        }

        return this.productService.searchByName(term).pipe(
          map(response => response.data ?? [])
        );

      })
    );


    const index = this.items.length;

    this.items.push(group);

    this.filteredProducts.push(filteredProducts$);


    group.get('quantity')!
      .valueChanges
      .subscribe(() => this.calculate(index));


    this.saleItems.push({
      form: group,
      productSearchControl,
      filteredProducts$: filteredProducts$
    });
  }

  removeItem(index: number): void {
    this.items.removeAt(index);
    this.filteredProducts.splice(index, 1);
    this.saleItems.splice(index, 1);
  }

  onProductSelected(index: number, product: ProductResponse): void {
    const alreadyExists = this.items.controls.some((control, currentIndex) => {
      if (currentIndex === index) {
        return false;
      }
      return control.get('productId')?.value === product.id;
    });
    if (alreadyExists) {
      return;
    }

    const group = this.items.at(index);
    group.patchValue({
      productId: product.id,
      unitPrice: product.price // ajuste o nome conforme sua interface
    });

    this.calculate(index);
  }

  calculate(index: number): void {
    const group = this.items.at(index);
    const quantity = group.get('quantity')!.value ?? 0;
    const price = group.getRawValue().unitPrice ?? 0;
    if (quantity > 20) {
      group.get('quantity')?.setErrors({
        maxQuantity: true
      });
      return;
    }
    let discountPercent = 0;
    if (quantity >= 10) {
      discountPercent = 20;
    } else if (quantity >= 4) {
      discountPercent = 10;
    }
    const subtotal = quantity * price;
    const discount = subtotal * discountPercent / 100;
    const total = subtotal - discount;

    group.patchValue({
      discount,
      total
    }, { emitEvent: false });
  }

  get totalSale(): number {
    return this.items.controls.reduce((sum, control) => {
      return sum + (control.getRawValue().total ?? 0);
    }, 0);
  }

  get totalDiscount(): number {
    return this.items.controls.reduce((sum, control) => {
      return sum + (control.getRawValue().discount ?? 0);
    }, 0);
  }

  save(): void {
    if (this.saleForm.invalid) {
      this.saleForm.markAllAsTouched();
      return;
    }
    const dto = {
      ...this.saleForm.getRawValue(),
      items: this.items.getRawValue()
    };

    if(this.isEdit) {
      this.saleService.update(this.saleId!, dto)
        .subscribe({
          next: () => {
            this.snackBar.open('Sale updated successfully!', 'Close', {
              duration: 3000
            });
            this.router.navigate(['']);
          },
          error: (error) => {
            console.error('Error updating sale:', error);
            this.snackBar.open('Error updating sale. Please try again.', 'Close', {
              duration: 3000
            });
          }
        });
    } else {
      this.saleService.create(dto)
        .subscribe({
          next: () => {
            this.snackBar.open('Sale created successfully!', 'Close', {
              duration: 3000
            });
            this.router.navigate(['']);
          },
          error: (error) => {
            console.error('Error creating sale:', error);
            this.snackBar.open('Error creating sale. Please try again.', 'Close', {
              duration: 3000
            });
          }
        });
    }
  }

  displayClient(value: ClientValueForm): string {
    if (!value) {
      return '';
    }
    if (typeof value === 'string') {
      return value;
    }
    return value.name ?? value.legalName ?? '';
  }

  displaySubsidiary(value: SubsidiaryValueForm): string {
    if (!value) {
      return '';
    }

    if (typeof value === 'string') {
      return value;
    }
    return value.tradeName ?? value.legalName ?? '';
  }

  onClientSelected(event: MatAutocompleteSelectedEvent): void {
    const client = event.option.value as ClientResponse;

    this.saleForm.patchValue({
      clientId: client.id
    });

    this.clientSearchControl.setValue(client, {
      emitEvent: false
    });
  }

  onSubsidiarySelected(event: MatAutocompleteSelectedEvent): void {
    const subsidiary = event.option.value as SubsidiaryResponse;

    this.saleForm.patchValue({
      subsidiaryId: subsidiary.id
    });

    this.subsidiarySearchControl.setValue(subsidiary, {
      emitEvent: false
    });
  }

  private configureAutocomplete(): void {
    this.filteredClients$ =
      this.clientSearchControl!
        .valueChanges.pipe(
          startWith(''),
          debounceTime(300),
          switchMap(value => {
            if (typeof value !== 'string') {
              return of([]);
            }
            if (value.length == 0) {
              return of([]);
            }
            return this.clientService.search(value).pipe(
              map((response: ApiOperationResponse<ClientResponse[]>) => response.data)
            );
          })
        );
    this.filteredSubsidiaries$ =
      this.subsidiarySearchControl!
        .valueChanges.pipe(
          startWith(''),
          debounceTime(300),
          switchMap(value => {
            if (typeof value !== 'string') {
              return of([]);
            }
            if (value.length == 0) {
              return of([]);
            }
            return this.subsidiaryService.search(value).pipe(
              map((response: ApiOperationResponse<SubsidiaryResponse[]>) => response.data)
            );
          })
        );
  }

  private loadSale(): void {
    this.saleService
      .getById(this.saleId!)
      .subscribe(response => {
        const sale = response.data;
        this.fillForm(sale);
      });
  }

  private fillForm(sale: SaleResponse): void {
    this.saleForm.patchValue({
      number: sale.number,
      clientId: sale.clientId,
      subsidiaryId: sale.subsidiaryId
    });

    this.clientSearchControl.setValue({
      id: sale.clientId,
      name: sale.clientName
    } as ClientResponse);

    this.subsidiarySearchControl.setValue({
      id: sale.subsidiaryId,
      tradeName: sale.subsidiaryName
    } as SubsidiaryResponse);
    this.disableFieldsOnEdit();
    this.items.clear();

    this.saleItems = [];

    for (const item of sale.items) {

      this.addItem();

      const group = this.items.at(this.items.length - 1);

      group.patchValue({
        productId: item.productId,
        code: item.productCode,
        quantity: item.quantity,
        unitPrice: item.unitPrice,
        discount: item.discount,
        total: item.total
      });

      this.saleItems[this.saleItems.length - 1]
        .productSearchControl
        .setValue({
          id: item.productId,
          code: item.productCode,
          name: item.productName,
          description: item.productDescription,
          price: item.unitPrice
        } as ProductResponse);
    }
  }

  private disableFieldsOnEdit(): void {
    this.saleForm.get('number')?.disable();
    this.clientSearchControl.disable();
    this.subsidiarySearchControl.disable();
  }
}
