import { Address } from './Address';
import { OrderItem } from './OrderItem';

export class Order {
  address: Address;
  deliveryMethod: string;
  deliveryPrice: number;
  orderItems: OrderItem[];
  subTotal: number;
  total: number;
  status: string;
  createdAt: Date;
}
