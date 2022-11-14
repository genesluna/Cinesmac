import { OrdersRoutingModule } from './orders-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders.component';
import { OrderDetailsComponent } from './order-details/order-details.component';

@NgModule({
  declarations: [OrdersComponent, OrderDetailsComponent],
  imports: [CommonModule, OrdersRoutingModule],
})
export class OrdersModule {}
