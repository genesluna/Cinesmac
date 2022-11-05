import { Component, OnInit } from '@angular/core';
import { Movie } from '../shared/models/movie';
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
    this.shopService.getMovies().subscribe(
      (response) => {
        this.movies = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
