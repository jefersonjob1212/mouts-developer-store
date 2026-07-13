import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleCreateComponent } from './sale-create.component';
import { SalesFormComponent } from '../../components/sales-form/sales-form.component';


@Component({
  selector: 'app-sales-form',
  standalone: true,
  template: ''
})
class SalesFormStubComponent {}

describe('SaleCreateComponent', () => {
  let component: SaleCreateComponent;
  let fixture: ComponentFixture<SaleCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        SaleCreateComponent
      ]
    })
    .overrideComponent(SaleCreateComponent, {
      remove: {
        imports: [
          SalesFormComponent
        ]
      },
      add: {
        imports: [
          SalesFormStubComponent
        ]
      }
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component)
      .toBeTruthy();
  });

});