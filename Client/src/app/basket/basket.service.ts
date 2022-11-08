import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket } from '../shared/models/Basket';
import { BasketItem } from '../shared/models/BasketItem';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = environment.apiUrl + 'baskets/';
  private basketSource = new BehaviorSubject<Basket>(null);
  basket$ = this.basketSource.asObservable();

  constructor(private http: HttpClient) {}

  addItemToBasket(item: BasketItem) {
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, item);
    this.setBasket(basket);
  }

  private addOrUpdateItem(items: BasketItem[], item: BasketItem): BasketItem[] {
    const index = items.findIndex((i) => i.id === item.id);

    if (index === -1) items.push(item);
    else items[index].quantity += item.quantity;

    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  getCurrentBasketValue(): Basket {
    return this.basketSource.value;
  }

  getBasket(id: string) {
    return this.http.get(this.baseUrl + id).pipe(
      map((basket: Basket) => {
        this.basketSource.next(basket);
      })
    );
  }

  setBasket(basket: Basket) {
    return this.http.post(this.baseUrl, basket).subscribe({
      next: (response: Basket) => this.basketSource.next(response),
      error: (error) => console.log(error),
    });
  }
}
