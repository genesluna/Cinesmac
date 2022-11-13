import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Address } from '../shared/models/Address';
import { User } from '../shared/models/User';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl + 'accounts/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  getCurrentUser(token: string) {
    if (localStorage.getItem('token') === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    return this.http.get(this.baseUrl).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  getUserAddress() {
    return this.http.get<Address>(this.baseUrl + 'address');
  }

  updateUserAddress(address: Address) {
    return this.http.put<Address>(this.baseUrl + 'address', address);
  }

  isEmailAvailable(email: string) {
    return this.http.get(this.baseUrl + 'is-email-available/' + email);
  }

  login(values: any) {
    return this.http.post(this.baseUrl + 'login', values).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/shop');
  }

  register(values: any) {
    return this.http.post(this.baseUrl + 'register', values).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }
}
