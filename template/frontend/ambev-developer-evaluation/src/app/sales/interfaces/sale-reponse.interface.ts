import { SaleStatus } from '../enums/sale-status.enum';
import { SaleItemResponse } from './sale-item-response.interface';

export interface SaleResponse {
  id: string;
  number: number;
  date: Date;
  clientId: string;
  clientName: string;
  subsidiaryId: string;
  subsidiaryName: string;
  status: SaleStatus;
  totalValues: number;
  items: SaleItemResponse[];
}