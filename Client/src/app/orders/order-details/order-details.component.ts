import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Order } from 'src/app/shared/models/Order';
import { OrderItem } from 'src/app/shared/models/OrderItem';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss'],
})
export class OrderDetailsComponent implements OnInit {
  order: Order;

  constructor(
    private route: ActivatedRoute,
    private ordersService: OrdersService
  ) {}

  ngOnInit(): void {
    this.getOrder(this.route.snapshot.paramMap.get('id'));
  }

  getOrder(orderId: string) {
    this.ordersService.getUserOrder(orderId).subscribe({
      next: (response: Order) => {
        this.order = response;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getTicketTypeName(ticketType: string): string {
    switch (ticketType) {
      case 'Full':
        return 'Inteira';
      case 'Half':
        return 'Meia';
      case 'Vip':
        return 'Vip';
    }
  }
}
