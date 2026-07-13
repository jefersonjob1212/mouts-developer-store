import { Component, Input } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesHomeComponent } from './sales-home.component';
import { SalesGridComponent } from '../../components/sales-grid/sales-grid.component';
import { SalesFilterComponent } from '../../components/sales-filter/sales-filter.component';
import { Router } from '@angular/router';
import { SaleFilter } from '@sales/interfaces/sale-filter-interface';


@Component({
  selector: 'app-sales-grid',
  standalone: true,
  template: ''
})
class SalesGridStubComponent {
  @Input() saleFilter!: SaleFilter;
}


@Component({
  selector: 'app-sales-filter',
  standalone: true,
  template: ''
})
class SalesFilterStubComponent {}

describe('SalesHomeComponent', () => {

  let component: SalesHomeComponent;
  let fixture: ComponentFixture<SalesHomeComponent>;

  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    routerSpy = jasmine.createSpyObj(
      'Router',
      ['navigate']
    );

    await TestBed.configureTestingModule({
      imports: [
        SalesHomeComponent
      ],
      providers: [
        {
          provide: Router,
          useValue: routerSpy
        }
      ]
    })
    .overrideComponent(SalesHomeComponent, {
      remove: {
        imports: [
          SalesGridComponent,
          SalesFilterComponent
        ]
      },
      add: {
        imports: [
          SalesGridStubComponent,
          SalesFilterStubComponent
        ]
      }
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalesHomeComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();

  });

  it('should create', () => {
    expect(component)
      .toBeTruthy();
  });

  it('should initialize saleFilter with default values', () => {
    expect(component.saleFilter())
      .toEqual({
        pageIndex: 1,
        pageSize: 10
      });
  });

  it('should update filter', () => {
    const filter = {
      pageIndex: 2,
      pageSize: 20,
      clientId: 'client-id'
    } as any;
    component.filter(filter);
    expect(component.saleFilter())
      .toEqual(filter);
  });

  it('should navigate to create sale', () => {
    component.routeToCreateSale();
    expect(routerSpy.navigate)
      .toHaveBeenCalledWith([
        '/sales/create'
      ]);

  });

});