import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUserOrders() {
    return this.http.get(this.baseUrl + 'orders');
  }

  getUserOrder(orderId: string) {
    return this.http.get(this.baseUrl + 'orders/' + orderId);
  }
}
