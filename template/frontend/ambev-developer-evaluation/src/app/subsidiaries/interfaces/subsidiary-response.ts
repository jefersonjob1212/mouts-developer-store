export interface SubsidiaryResponse {
  /**
   * Unique ID of subsidiary
   */
  id: string;

  /**
   * Unique CNPJ of subsidiary
   */
  cnpj: string;

  /**
   * Legal name of subsidiary
   */
  legalName: string;

  /**
   * Trade name of subsidiary
   */
  tradeName: string;

  /**
   * Address of subsidiary
   */
  address: string;

  /**
   * City of subsidiary
   */
  city: string;

  /**
   * State of subsidiary
   */
  state: string;
}