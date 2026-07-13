import { AfterViewInit, Component, effect, EventEmitter, inject, input, OnInit, Output, signal, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { SalesService } from '@sales/services/sales.service';
import { SaleResponse } from '@sales/interfaces/sale-reponse.interface';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { SaleFilter } from '@sales/interfaces/sale-filter-interface';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmCancelComponent } from '../confirm-cancel/confirm-cancel.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-sales-grid',
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    CommonModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './sales-grid.component.html',
  styleUrl: './sales-grid.component.scss'
})
export class SalesGridComponent implements OnInit {
  private snackBar = inject(MatSnackBar);
  private salesService = inject(SalesService);
  readonly dialog = inject(MatDialog);
  saleFilter = input<SaleFilter>();
  salesList = signal<SaleResponse[]>([]);  
  displayedColumns: string[] = ['number', 'date', 'clientName', 'subsidiaryName', 'totalValues', 'status', 'actions'];

  totalItems = signal(0);

  filter = {
    page: 1,
    pageSize: 5
  };
  
  constructor() {
    effect(() => {
      const filter = this.saleFilter();
      if (filter) {
        this.loadSales(filter);
      }
    });
  }

  ngOnInit(): void {
    this.loadSales({ pageIndex: 1, pageSize: 5 });
  }

  onEdit(sale: SaleResponse): void {
    
  }

  onCancel(id: string): void {
    const dialogRef = this.dialog.open(ConfirmCancelComponent, {
      data: id
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result)
      if (result !== undefined) {
        this.salesService.cancel(result.id).subscribe({
          next: () => {
            this.snackBar.open('Sale cancelled successfully', 'Close');
            this.loadSales({ pageIndex: this.filter.page, pageSize: this.filter.pageSize });
          }
        })
      }
    });
  }


  onPageChange(event: PageEvent): void {

    this.filter.page = event.pageIndex + 1;
    this.filter.pageSize = event.pageSize;

    this.loadSales({ pageIndex: this.filter.page, pageSize: this.filter.pageSize });

  }

  defineStatus(status: number): string {
    let statusText: string = '';
    switch (status) {
      case 0:
        statusText = 'Created';
        break;
      case 1:
        statusText = 'Approved';
        break;
      case 2:
        statusText = 'Canceled';
        break;
    }
    return statusText;
  }

  private loadSales(filter: SaleFilter): void {
    this.salesService.getPaginatedByParam(filter).subscribe({
      next: (sales) => {
        this.salesList.set(sales.data.data);
        this.totalItems.set(sales.data.totalCount);
      },
      error: (error) => {
        console.error('Error fetching sales:', error);
      }
    });
  }
}
