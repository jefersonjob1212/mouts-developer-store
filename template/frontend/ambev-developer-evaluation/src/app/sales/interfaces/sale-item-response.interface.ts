import { SaleItemStatus } from '../enums/sale-item-status.enum';

export interface SaleItemResponse {
  productName: string;
  unitPrice: number;
  quantity: number;
  discount: number;
  total: number;
  status: SaleItemStatus;
}