import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  selectedSessionId: string;
  selectedTicketTypeId: number;
  selectedPrice: number;
  selectedSessionPrices = {};
  sessionPrices = [];
  ticketQuantity = 1;
  ticketsAvailable = 15;

  constructor(
    private shopService: ShopService,
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
      },
      error: (error) => console.log(error),
    });
  }

  onSessionSelected(sessionId: string): void {
    this.selectedSessionId = sessionId;
    this.selectedSessionPrices = this.sessionPrices.find(
      ({ sessionId }) => sessionId === this.selectedSessionId
    );
  }

  onTicketTypeSelected(ticketId: number) {
    this.selectedTicketTypeId = ticketId;
    this.selectedPrice =
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
    console.log(this.sessionPrices);
  }

  addTicketToBasket(): void {
    // TODO
    console.log('Not implemented, yet! :/');
  }

  incrementTicketQuantity(): void {
    if (this.ticketQuantity < this.ticketsAvailable) {
      this.ticketQuantity++;
    }
  }

  decrementTicketQuantity(): void {
    if (this.ticketQuantity > 1) {
      this.ticketQuantity--;
    }
  }
}
