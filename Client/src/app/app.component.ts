import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Cinesmac';

  constructor(
    private basketService: BasketService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getBasket();
    this.getCurretUser();
  }

  private getBasket() {
    const basketId = localStorage.getItem('basket_id');

    if (basketId) {
      this.basketService.getBasket(basketId).subscribe({
        error: (error) => console.log(error),
      });
    }
  }

  getCurretUser() {
    const token = localStorage.getItem('token');

    this.accountService.getCurrentUser(token).subscribe({
      error: (error) => console.log(error),
    });
  }
}
