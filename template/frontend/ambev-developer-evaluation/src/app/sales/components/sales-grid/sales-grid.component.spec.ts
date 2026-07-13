import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';

import { SalesGridComponent } from './sales-grid.component';
import { SalesService } from '@sales/services/sales.service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

describe('SalesGridComponent', () => {

  let component: SalesGridComponent;
  let fixture: ComponentFixture<SalesGridComponent>;

  let salesServiceSpy: jasmine.SpyObj<SalesService>;
  let dialogSpy: jasmine.SpyObj<MatDialog>;
  let snackBarSpy: jasmine.SpyObj<MatSnackBar>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {

    salesServiceSpy = jasmine.createSpyObj('SalesService', [
      'getPaginatedByParam',
      'cancel'
    ]);

    dialogSpy = jasmine.createSpyObj('MatDialog', [
      'open'
    ]);

    snackBarSpy = jasmine.createSpyObj('MatSnackBar', [
      'open'
    ]);

    routerSpy = jasmine.createSpyObj('Router', [
      'navigate'
    ]);

    salesServiceSpy.getPaginatedByParam.and.returnValue(
      of({
        data: {
          data: [
            {
              id: '1',
              number: 10,
              clientName: 'John'
            }
          ],
          totalCount: 1
        }
      } as any)
    );

    await TestBed.configureTestingModule({
      imports: [
        SalesGridComponent
      ],
      providers: [
        {
          provide: SalesService,
          useValue: salesServiceSpy
        },
        {
          provide: MatDialog,
          useValue: dialogSpy
        },
        {
          provide: MatSnackBar,
          useValue: snackBarSpy
        },
        {
          provide: Router,
          useValue: routerSpy
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SalesGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load sales on init', () => {
    expect(
      salesServiceSpy.getPaginatedByParam
    )
      .toHaveBeenCalledWith({
        pageIndex: 1,
        pageSize: 5
      });

    expect(component.salesList().length)
      .toBe(1);

    expect(component.totalItems())
      .toBe(1);
  });

  it('should update sales when filter input changes', () => {
    salesServiceSpy.getPaginatedByParam.calls.reset();

    
    fixture.componentRef.setInput('saleFilter', {
      pageIndex: 2,
      pageSize: 10,
      clientId: 'client-id'
    } as any);

    fixture.detectChanges();

    expect(
      salesServiceSpy.getPaginatedByParam
    )
      .toHaveBeenCalledWith({
        pageIndex: 2,
        pageSize: 10,
        clientId: 'client-id'
      });
  });

  it('should navigate to edit sale', () => {
    const sale: any = {
      id: 'sale-id'
    };

    component.onEdit(sale);

    expect(routerSpy.navigate)
      .toHaveBeenCalledWith([
        'sales/edit',
        'sale-id'
      ]);
  });

  it('should change page and reload sales', () => {
    salesServiceSpy.getPaginatedByParam.calls.reset();

    component.onPageChange({
      pageIndex: 1,
      pageSize: 10
    } as any);

    expect(component.filter.page)
      .toBe(2);

    expect(component.filter.pageSize)
      .toBe(10);

    expect(
      salesServiceSpy.getPaginatedByParam
    )
      .toHaveBeenCalledWith({
        pageIndex: 2,
        pageSize: 10
      });

  });

  describe('defineStatus', () => {

    it('should return Created', () => {
      expect(
        component.defineStatus(0)
      )
        .toBe('Created');
    });

    it('should return Approved', () => {
      expect(
        component.defineStatus(1)
      )
        .toBe('Approved');
    });

    it('should return Canceled', () => {
      expect(
        component.defineStatus(2)
      )
        .toBe('Canceled');
    });

    it('should return empty for unknown status', () => {
      expect(
        component.defineStatus(99)
      )
        .toBe('');
    });
  });


  it('should cancel sale after dialog confirmation', () => {
    const dialogRefSpy = {
      afterClosed: () => of({
        id: 'sale-id'
      })
    };

    dialogSpy.open.and.returnValue(
      dialogRefSpy as any
    );
    salesServiceSpy.cancel.and.returnValue(
      of({} as any)
    );

    component.onCancel('sale-id');

    expect(dialogSpy.open)
      .toHaveBeenCalled();

    expect(salesServiceSpy.cancel)
      .toHaveBeenCalledWith('sale-id');

    expect(snackBarSpy.open)
      .toHaveBeenCalledWith(
        'Sale cancelled successfully',
        'Close'
      );
  });

  it('should not cancel sale when dialog is closed without result', () => {
    dialogSpy.open.and.returnValue({
      afterClosed: () => of(undefined)
    } as any);

    component.onCancel('sale-id');

    expect(
      salesServiceSpy.cancel
    )
      .not.toHaveBeenCalled();
  });
});