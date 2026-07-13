import { Component, inject, signal } from '@angular/core';
import { SalesGridComponent } from "../../components/sales-grid/sales-grid.component";
import { SalesFilterComponent } from '../../components/sales-filter/sales-filter.component';
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from '@angular/material/button';
import { SaleFilter } from '@sales/interfaces/sale-filter-interface';
import { Router } from '@angular/router';
import { ConfirmCancelComponent } from '@sales/components/confirm-cancel/confirm-cancel.component';
import { MatDialog } from '@angular/material/dialog';
import { SalesService } from '@sales/services/sales.service';

@Component({
  selector: 'app-sales-home',
  imports: [
    SalesGridComponent, 
    SalesFilterComponent, 
    MatIconModule, 
    MatButtonModule
  ],
  templateUrl: './sales-home.component.html',
  styleUrl: './sales-home.component.scss'
})
export class SalesHomeComponent {
  private router = inject(Router);
  
  saleFilter = signal<SaleFilter>({ pageIndex: 1, pageSize: 10 });
  
  filter(filter: SaleFilter): void {
    this.saleFilter.set(filter);
  }

  routeToCreateSale(): void {
    this.router.navigate(['/sales/create']);
  }
}
