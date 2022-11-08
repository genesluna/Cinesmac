import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/BasketItem';
import { Movie } from 'src/app/shared/models/Movie';
import { Session } from 'src/app/shared/models/Session';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.scss'],
})
export class MovieDetailsComponent implements OnInit {
  movie: Movie;
  basketItem: BasketItem = new BasketItem();
  addedToBasket: boolean = false;
  selectedSessionPrices = {};
  sessionPrices = [];
  ticketsAvailable = 15;

  constructor(
    private shopService: ShopService,
    private basketService: BasketService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getMovie();
  }

  getMovie(): void {
    const movieId = this.activatedRoute.snapshot.paramMap.get('id');

    this.shopService.getMovie(movieId).subscribe({
      next: (response) => {
        this.movie = response;
        this.sortSessions(this.movie.sessions);
        this.setSessionPrices(this.movie.sessions);
        this.basketItem.movieTitle = this.movie.title;
        this.basketItem.imageUrl = this.movie.imageURL;
      },
      error: (error) => console.log(error),
    });
  }

  onSessionSelected(sessionId: string): void {
    if (this.addedToBasket) return;

    this.basketItem.id = sessionId;
    this.selectedSessionPrices = this.sessionPrices.find(
      ({ sessionId }) => sessionId === this.basketItem.id
    );
    const session = this.movie.sessions.find((s) => s.id === sessionId);
    this.basketItem.sessionStartTime = session.starTime;
    this.basketItem.screeningRoomName = session.screeningRoom.name;
  }

  onTicketTypeSelected(ticketId: number) {
    if (this.addedToBasket) return;

    this.basketItem.ticketType = ticketId;
    this.basketItem.price =
      this.selectedSessionPrices['ticket'][ticketId].ticketPrice;
  }

  sortSessions(sessions: Session[]): void {
    sessions.sort((a, b) => a.starTime - b.starTime);
  }

  setSessionPrices(sessions: Session[]): void {
    let onlyVip = this.movie.is3D || this.movie.isIMAX;

    sessions.forEach((session) =>
      this.sessionPrices.push({
        sessionId: session.id,
        ticket: [
          {
            id: 0,
            ticketType: 'Inteira',
            ticketPrice: session.basePrice,
            disabled: onlyVip,
          },
          {
            id: 1,
            ticketType: 'Meia',
            ticketPrice: session.halfPrice,
            disabled: onlyVip,
          },
          {
            id: 2,
            ticketType: 'Vip',
            ticketPrice: session.vipPrice,
            disabled: false,
          },
        ],
      })
    );
  }

  addTicketToBasket(): void {
    this.basketService.addItemToBasket(this.basketItem);
    this.addedToBasket = true;
  }

  incrementTicketQuantity(): void {
    if (this.addedToBasket) return;

    if (this.basketItem.quantity < this.ticketsAvailable) {
      this.basketItem.quantity++;
    }
  }

  decrementTicketQuantity(): void {
    if (this.addedToBasket) return;
    if (this.basketItem.quantity > 1) {
      this.basketItem.quantity--;
    }
  }
}
