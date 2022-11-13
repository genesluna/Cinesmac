import { v4 as uuidv4 } from 'uuid';
import { BasketItem } from './BasketItem';

export class Basket {
  id: string = uuidv4();
  subTotal: number = 0;
  shippingPrice: number = 0;
  total: number = 0;
  items: BasketItem[] = [];
  deliveryMethodId?: string;
  clientSecret?: string;
  paymentIntentId?: string;
}
