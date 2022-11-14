import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket } from '../shared/models/Basket';
import { BasketItem } from '../shared/models/BasketItem';
import { DeliveryMethod } from '../shared/models/DeliveryMethod';

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

  private calculateTotals(basket: Basket) {
    basket.subTotal = basket.items.reduce(
      (a, b) => b.price * b.quantity + a,
      0
    );
    basket.total = basket.subTotal + basket.shippingPrice;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  createPaymentIntent() {
    return this.http
      .post(
        environment.apiUrl + 'payments/' + this.getCurrentBasketValue().id,
        {}
      )
      .pipe(
        map((basket: Basket) => {
          this.basketSource.next(basket);
        })
      );
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

  incrementItemQuantity(item: BasketItem) {
    const basket = this.getCurrentBasketValue();
    const itemIndex = basket.items.findIndex((x) => x.id === item.id);
    basket.items[itemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuantity(item: BasketItem) {
    const basket = this.getCurrentBasketValue();
    const itemIndex = basket.items.findIndex((x) => x.id === item.id);

    if (basket.items[itemIndex].quantity > 1) {
      basket.items[itemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  deleteBasket(id: string) {
    return this.http.delete(this.baseUrl + id).subscribe({
      next: () => {
        this.deleteLocalBasket(id);
      },
      error: (error) => console.log(error),
    });
  }

  deleteLocalBasket(id: string) {
    this.basketSource.next(null);
    localStorage.removeItem('basket_id');
  }

  removeItemFromBasket(item: BasketItem) {
    const basket = this.getCurrentBasketValue();

    if (basket.items.some((x) => x.id === item.id)) {
      basket.items = basket.items.filter((i) => i.id !== item.id);

      if (basket.items.length > 0) this.setBasket(basket);
      else this.deleteBasket(basket.id);
    }
  }

  setBasket(basket: Basket) {
    this.calculateTotals(basket);
    return this.http.post(this.baseUrl, basket).subscribe({
      next: (response: Basket) => {
        this.basketSource.next(response);
      },
      error: (error) => console.log(error),
    });
  }

  setShippingPrice(deliveryMethod: DeliveryMethod) {
    const basket = this.getCurrentBasketValue();
    basket.shippingPrice = deliveryMethod.price;
    basket.deliveryMethodId = deliveryMethod.id;
    this.calculateTotals(basket);
    this.basketSource.next(basket);
  }
}
