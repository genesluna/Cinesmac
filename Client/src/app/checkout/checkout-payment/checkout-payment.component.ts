import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnDestroy,
  ViewChild,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from 'src/app/shared/models/Basket';
import { Order } from 'src/app/shared/models/Order';
import { OrderCreate } from 'src/app/shared/models/OrderCreate';
import { CheckoutService } from '../checkout.service';
import { loadStripe } from '@stripe/stripe-js/pure';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent implements AfterViewInit, OnDestroy {
  @Input() checkoutForm: FormGroup;
  @ViewChild('cardNumber', { static: true }) cardNumberElement: ElementRef;
  @ViewChild('cardExpiry', { static: true }) cardExpiryElement: ElementRef;
  @ViewChild('cardCvc', { static: true }) cardCvcElement: ElementRef;

  stripe: any;
  cardNumber: any;
  cardNumberValid: boolean = false;
  cardExpiry: any;
  cardExpiryValid: boolean = false;
  cardCvc: any;
  cardCvcValid: boolean = false;
  cardErrors: any;
  cardHandler = this.onChange.bind(this);
  loading: boolean = false;

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private router: Router
  ) {
    loadStripe.setLoadParameters({ advancedFraudSignals: false });
  }

  async ngAfterViewInit(): Promise<void> {
    this.stripe = await loadStripe(
      'pk_test_51M33rXHzQsFoPrBPQxg3XFZldTIFJtw5fb3WKwprLRaOYxN1IN2GhBhiux60Siskb53P7e4b3V1XjsPMVzg62yNJ00QUR5kZoM'
    );
    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    this.cardNumber.addEventListener('change', this.cardHandler);

    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    this.cardExpiry.addEventListener('change', this.cardHandler);

    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.cardCvcElement.nativeElement);
    this.cardCvc.addEventListener('change', this.cardHandler);
  }

  ngOnDestroy(): void {
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
  }

  private async confirmPayment(basket: Basket) {
    return this.stripe.confirmCardPayment(basket.clientSecret, {
      payment_method: {
        card: this.cardNumber,
        billing_details: {
          name: this.checkoutForm.get('paymentForm').get('nameOnCard').value,
        },
      },
    });
  }

  private async createOrder(basket: Basket) {
    const orderToCreate = this.getOrderToCreate(basket);
    return this.checkoutService.createOrder(orderToCreate).toPromise();
  }

  private getOrderToCreate(basket: Basket): OrderCreate {
    const order = new OrderCreate();
    order.basketId = basket.id;

    return <OrderCreate>{
      basketId: basket.id,
      deliveryMethodId: this.checkoutForm
        .get('deliveryForm')
        .get('deliveryMethod').value,
      orderAddress: this.checkoutForm.get('addressForm').value,
    };
  }

  isFormValid(): boolean {
    return (
      this.checkoutForm.get('paymentForm').valid &&
      this.cardNumberValid &&
      this.cardExpiryValid &&
      this.cardCvcValid
    );
  }

  onChange(event) {
    if (event.error) {
      this.cardErrors = event.error.message;
    } else {
      this.cardErrors = null;
    }

    switch (event.elementType) {
      case 'cardNumber':
        this.cardNumberValid = event.complete;
        break;
      case 'cardExpiry':
        this.cardExpiryValid = event.complete;
        break;
      case 'cardCvc':
        this.cardCvcValid = event.complete;
        break;
    }
  }

  async submitOrder() {
    this.loading = true;
    const basket = this.basketService.getCurrentBasketValue();

    try {
      const createdOrder = await this.createOrder(basket);
      const paymentResult = await this.confirmPayment(basket);

      if (paymentResult.paymentIntent) {
        this.basketService.deleteLocalBasket(basket.id);
        const navExtras: NavigationExtras = { state: createdOrder };
        this.router.navigate(['checkout/success'], navExtras);
      } else {
        this.toastr.error(paymentResult.error.message);
      }
    } catch (error) {
      console.log(error);
    } finally {
      this.loading = false;
    }
  }
}
