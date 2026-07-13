import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleItemCreateComponent } from './sale-item-create.component';

describe('SaleItemCreateComponent', () => {
  let component: SaleItemCreateComponent;
  let fixture: ComponentFixture<SaleItemCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleItemCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleItemCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
