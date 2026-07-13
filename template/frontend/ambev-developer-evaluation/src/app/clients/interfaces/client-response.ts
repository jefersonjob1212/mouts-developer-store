import { GenderIndividualClient } from "@clients/enums/gender-individual-client.enum";

export interface ClientResponse {
  /**
   * The unique identifier of the client
   */
  id: string;

  /**
   * The client phone number
   */
  phoneNumber: string;

  /**
   * The client email
   */
  email: string;

  /**
   * The client address
   */
  address: string;

  /**
   * The client city
   */
  city: string;

  /**
   * The client state
   */
  state: string;

  /**
   * The individual client CPF
   */
  cpf?: string;

  /**
   * The individual client name
   */
  name?: string;

  /**
   * The individual client born date
   */
  bornDate?: string;

  /**
   * The individual client gender
   */
  gender?: GenderIndividualClient;

  /**
   * The company client CNPJ
   */
  cnpj?: string;

  /**
   * The company client legal name
   */
  legalName?: string;

  /**
   * The company client trade name
   */
  tradeName?: string;
}