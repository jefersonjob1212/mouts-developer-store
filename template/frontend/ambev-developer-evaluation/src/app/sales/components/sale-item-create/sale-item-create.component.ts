import { AsyncPipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ProductResponse } from '@products/interfaces/product-response';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sale-item-create',
  imports: [
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    AsyncPipe
  ],
  templateUrl: './sale-item-create.component.html',
  styleUrl: './sale-item-create.component.scss'
})
export class SaleItemCreateComponent {

  @Input({ required: true })
  group!: FormGroup;

  @Input({ required: true })
  productSearchControl!: FormControl<ProductResponse | string | null>;

  @Input({ required: true })
  filteredProducts$!: Observable<ProductResponse[]>;

  @Output()
  remove = new EventEmitter<void>();

  @Output()
  productSelected = new EventEmitter<ProductResponse>();

  displayProduct(value: ProductResponse | string | null): string {
    if (!value) {
      return '';
    }

    if (typeof value === 'string') {
      return value;
    }
    return `${value.code} - ${value.name}`;
  }

  onProductSelected(event: MatAutocompleteSelectedEvent): void {
    const product = event.option.value as ProductResponse;
    this.productSelected.emit(product);

  }

  removeItem(): void {
    this.remove.emit();
  }

}
