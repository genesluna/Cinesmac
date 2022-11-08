import { Observable } from 'rxjs/internal/Observable';
import { BasketService } from './../../basket/basket.service';
import { Component, OnInit } from '@angular/core';
import { Basket } from 'src/app/shared/models/Basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  basket$: Observable<Basket>;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }
}
