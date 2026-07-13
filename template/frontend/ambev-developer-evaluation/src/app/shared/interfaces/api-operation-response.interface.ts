export interface ApiOperationResponse<T> {
  success: boolean;
  message: string;
  data: T;
}