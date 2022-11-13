import { Address } from './Address';

export class OrderCreate {
  deliveryMethodId: string;
  basketId: string;
  orderAddress: Address;
}
