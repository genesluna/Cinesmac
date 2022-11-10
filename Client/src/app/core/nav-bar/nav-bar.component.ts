import { Observable } from 'rxjs/internal/Observable';
import { BasketService } from './../../basket/basket.service';
import { Component, OnInit } from '@angular/core';
import { Basket } from 'src/app/shared/models/Basket';
import { User } from 'src/app/shared/models/User';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  basket$: Observable<Basket>;
  currentUser$: Observable<User>;

  constructor(
    private basketService: BasketService,
    private accountServive: AccountService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.accountServive.currentUser$;
  }

  getFirstName(name: string) {
    const namesArray = name.split(' ');
    return namesArray[0];
  }

  logout() {
    this.accountServive.logout();
  }
}
