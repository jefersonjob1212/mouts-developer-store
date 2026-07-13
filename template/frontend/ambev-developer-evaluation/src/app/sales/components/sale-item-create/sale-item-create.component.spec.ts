import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

import { SaleItemCreateComponent } from './sale-item-create.component';

describe('SaleItemCreateComponent', () => {
  let component: SaleItemCreateComponent;
  let fixture: ComponentFixture<SaleItemCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        SaleItemCreateComponent
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SaleItemCreateComponent);
    component = fixture.componentInstance;

    component.group = new FormGroup({
      productId: new FormControl(null),
      quantity: new FormControl(1),
      unitPrice: new FormControl({
        value: 0,
        disabled: true
      }),
      discount: new FormControl({
        value: 0,
        disabled: true
      }),
      total: new FormControl({
        value: 0,
        disabled: true
      })
    });

    component.productSearchControl = new FormControl(null);

    component.filteredProducts$ = of([]);

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('displayProduct', () => {

    it('should return empty string when value is null', () => {
      expect(component.displayProduct(null))
        .toBe('');
    });

    it('should return value when product is a string', () => {
      expect(component.displayProduct('Notebook'))
        .toBe('Notebook');
    });

    it('should return formatted product when value is an object', () => {
      const product: any = {
        code: '001',
        name: 'Notebook'
      };

      expect(component.displayProduct(product))
        .toBe('001 - Notebook');
    });

  });

  it('should emit productSelected when product is selected', () => {
    const product: any = {
      id: '1',
      code: '001',
      name: 'Notebook'
    };

    spyOn(component.productSelected, 'emit');

    const event = {
      option: {
        value: product
      }
    } as MatAutocompleteSelectedEvent;

    component.onProductSelected(event);

    expect(component.productSelected.emit)
      .toHaveBeenCalledWith(product);
  });

  it('should emit remove event when removing item', () => {
    spyOn(component.remove, 'emit');

    component.removeItem();

    expect(component.remove.emit)
      .toHaveBeenCalled();
  });

  it('should have product search control initialized', () => {
    expect(component.productSearchControl)
      .toBeTruthy();
  });

  it('should have filtered products observable initialized', () => {
    expect(component.filteredProducts$)
      .toBeTruthy();
  });

});