import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdministratorService {

  private adminURL = 'http://localhost:5000/api/administrators';

  constructor(private http: HttpClient) { }

  addAdministrator(email: string, name: string, password: string) {
    return this.http.post(this.adminURL, {
      Email: email,
      Name: name,
      Password: password
    });
  }
}
