import { SaleStatus } from '../enums/sale-status.enum';
import { SaleItemResponse } from './sale-item-response.interface';

export interface SaleResponse {
  id: string;
  number: number;
  date: Date;
  clientName: string;
  subsidiaryName: string;
  status: SaleStatus;
  totalValues: number;
  items: SaleItemResponse[];
}