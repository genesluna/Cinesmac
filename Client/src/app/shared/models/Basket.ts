import { v4 as uuidv4 } from 'uuid';
import { BasketItem } from './BasketItem';

export class Basket {
  id: string = uuidv4();
  total: number = 0;
  items: BasketItem[] = [];
}
