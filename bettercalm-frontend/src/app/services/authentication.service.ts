import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalVariables } from '../globals';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private loginURL = `${GlobalVariables.BASE_API_URL}/administrators/authenticate`;

  constructor(private http: HttpClient) { }

  login(email: string, pass: string) {
    return this.http.post(this.loginURL, {
      email: email,
      password: pass
    });
  }

  logout() {
    localStorage.clear();
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    localStorage.getItem('token');
  }

  isAuthenticated() {
    return localStorage.getItem('token') != null;
  }
}
