import { Component } from '@angular/core';
import { SalesFormComponent } from "../../components/sales-form/sales-form.component";

@Component({
  selector: 'app-sale-create',
  imports: [SalesFormComponent],
  templateUrl: './sale-create.component.html',
  styleUrl: './sale-create.component.scss'
})
export class SaleCreateComponent {

}
