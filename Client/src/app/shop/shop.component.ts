import { Component, OnInit } from '@angular/core';
import { Movie } from '../shared/models/Movie';
import { MovieListQueryParams } from '../shared/models/MovieListQueryParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  movies: Movie[];
  itemsCount: number;
  pagesCount: number;
  queryParams = new MovieListQueryParams();

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getMovies();
  }

  private getMovies() {
    this.shopService.getMovies(this.queryParams).subscribe({
      next: (response) => {
        this.queryParams.index = parseInt(
          response.headers.get('pagination-index')
        );
        this.queryParams.limit = parseInt(
          response.headers.get('pagination-limit')
        );
        this.itemsCount = parseInt(
          response.headers.get('pagination-items-count')
        );
        this.pagesCount = parseInt(
          response.headers.get('pagination-pages-count')
        );
        this.movies = response.body;
      },
      error: (error) => console.log(error),
    });
  }

  onPageChange(event: { page: number; itemsPerPage: number }) {
    this.queryParams.index = event.page;
    this.getMovies();
  }
}
