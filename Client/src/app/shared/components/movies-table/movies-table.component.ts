import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from '../../models/Basket';
import { BasketItem } from '../../models/BasketItem';

@Component({
  selector: 'app-movies-table',
  templateUrl: './movies-table.component.html',
  styleUrls: ['./movies-table.component.scss'],
})
export class MoviesTableComponent implements OnInit {
  @Input() isBasket: boolean = true;
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
