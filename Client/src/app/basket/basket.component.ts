import { BasketItem } from 'src/app/shared/models/BasketItem';
import { BasketService } from 'src/app/basket/basket.service';
import { Observable } from 'rxjs/internal/Observable';
import { Component, OnInit } from '@angular/core';
import { Basket } from '../shared/models/Basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent implements OnInit {
  basket$: Observable<Basket>;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  decrementItemQuantity(item: BasketItem) {
    this.basketService.decrementItemQuantity(item);
  }

  incrementItemQuantity(item: BasketItem) {
    this.basketService.incrementItemQuantity(item);
  }

  getTicketTypeName(ticketType: number): string {
    switch (ticketType) {
      case 0:
        return 'Inteira';
      case 1:
        return 'Meia';
      case 2:
        return 'Vip';

      default:
        return '';
    }
  }

  removeItemFromBasket(item: BasketItem) {
    this.basketService.removeItemFromBasket(item);
  }
}
