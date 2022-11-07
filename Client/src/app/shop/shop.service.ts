import { MovieListQueryParams } from './../shared/models/MovieListQueryParams';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Movie } from '../shared/models/Movie';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = environment.apiUrl + 'movies/';

  constructor(private http: HttpClient) {}

  getMovies(
    queryParams: MovieListQueryParams
  ): Observable<HttpResponse<Movie[]>> {
    let params = new HttpParams();

    params = params.append('index', queryParams.index.toString());
    params = params.append('limit', queryParams.limit.toString());

    return this.http.get<Movie[]>(this.baseUrl, {
      observe: 'response',
      params,
    });
  }

  getMovie(id: string): Observable<Movie> {
    return this.http.get<Movie>(this.baseUrl + id);
  }
}
