export interface SaleFilter {
  number?: number | null;
  clientId?: string | null;
  subsidiaryId?: string | null;
  pageIndex: number;
  pageSize: number;
}