import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Movie } from '../shared/models/Movie';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/v1/';

  constructor(private http: HttpClient) {}

  getMovies(): Observable<HttpResponse<Movie[]>> {
    return this.http.get<Movie[]>(this.baseUrl + 'movies', {
      observe: 'response',
    });
  }
}
