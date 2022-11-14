import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/models/Order';
import { OrdersService } from './orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit {
  orders: Order[];

  constructor(private ordersService: OrdersService, private router: Router) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.ordersService.getUserOrders().subscribe({
      next: (response: Order[]) => {
        this.orders = response;
      },
      error: (error) => console.log(error),
    });
  }

  getOrderStatus(staus: string): string {
    switch (staus) {
      case 'Pending':
        return 'Pagamento pendente';
      case 'Payment Received':
        return 'Pagamento recebido';
      case 'Payment Failed':
        return 'Falha no pagamento';
    }
  }
}
