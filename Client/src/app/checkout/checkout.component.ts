import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from '../basket/basket.service';
import { Basket } from '../shared/models/Basket';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  basket$: Observable<Basket>;
  checkoutForm: FormGroup;

  constructor(
    private basketService: BasketService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.createCheckoutForm();
    this.getAddressFormValues();
  }

  createCheckoutForm() {
    this.checkoutForm = new FormGroup({
      addressForm: new FormGroup({
        street: new FormControl('', [Validators.required]),
        city: new FormControl('', [Validators.required]),
        state: new FormControl('', [Validators.required]),
        zipCode: new FormControl('', [Validators.required]),
      }),
      deliveryForm: new FormGroup({
        deliveryMethod: new FormControl('', [Validators.required]),
      }),
      paymentForm: new FormGroup({
        nameOnCard: new FormControl('', [Validators.required]),
      }),
    });
  }

  getAddressFormValues() {
    this.accountService.getUserAddress().subscribe({
      next: (response) => {
        if (response) this.checkoutForm.get('addressForm').patchValue(response);
      },
      error: (error) => console.log(error),
    });
  }
}
