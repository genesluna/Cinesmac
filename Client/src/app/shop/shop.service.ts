import { MovieListQueryParams } from './../shared/models/MovieListQueryParams';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Movie } from '../shared/models/Movie';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/v1/';

  constructor(private http: HttpClient) {}

  getMovies(
    queryParams: MovieListQueryParams
  ): Observable<HttpResponse<Movie[]>> {
    let params = new HttpParams();

    params = params.append('index', queryParams.index.toString());
    params = params.append('limit', queryParams.limit.toString());

    return this.http.get<Movie[]>(this.baseUrl + 'movies', {
      observe: 'response',
      params,
    });
  }
}
