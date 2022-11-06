import { Component, OnInit } from '@angular/core';
import { Movie } from '../shared/models/Movie';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  movies: Movie[];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getMovies();
  }

  private getMovies() {
    this.shopService.getMovies().subscribe({
      next: (response) => (this.movies = response.body),
      error: (error) => console.log(error),
    });
  }
}
