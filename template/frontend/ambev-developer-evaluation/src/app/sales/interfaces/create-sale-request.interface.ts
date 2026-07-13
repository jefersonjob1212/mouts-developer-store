export interface CreateUpdateSaleRequest {
  number: number;
  date: Date;
  clientId: string;
  subsidiaryId: string;
  items: CreateUpdateSaleItemRequest[];
}

export interface CreateUpdateSaleItemRequest {
  productId: string;
  quantity: number;
}