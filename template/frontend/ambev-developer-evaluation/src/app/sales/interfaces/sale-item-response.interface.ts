import { SaleItemStatus } from '../enums/sale-item-status.enum';

export interface SaleItemResponse {
  productId: string;
  productCode: string;
  productName: string;
  productDescription: string;
  unitPrice: number;
  quantity: number;
  discount: number;
  total: number;
  status: SaleItemStatus;
}