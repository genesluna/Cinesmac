import { FormGroup } from '@angular/forms';
import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { ToastrService } from 'ngx-toastr';
import { Address } from 'src/app/shared/models/Address';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss'],
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm: FormGroup;

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  saveUserAddress() {
    this.accountService
      .updateUserAddress(this.checkoutForm.get('addressForm').value)
      .subscribe({
        next: (address: Address) => {
          this.toastr.success('Endereço salvo');
          this.checkoutForm.get('addressForm').reset(address);
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
