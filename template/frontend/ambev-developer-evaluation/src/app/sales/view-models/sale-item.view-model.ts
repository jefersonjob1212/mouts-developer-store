import { FormControl, FormGroup } from "@angular/forms";
import { ProductResponse } from "@products/interfaces/product-response";
import { Observable } from "rxjs";

export interface SaleItemViewModel {
    form: FormGroup<{
        productId: FormControl<string | null>;
        quantity: FormControl<number>;
    }>;
    productSearchControl: FormControl<ProductResponse | string | null>;
    filteredProducts$: Observable<ProductResponse[]>;
}