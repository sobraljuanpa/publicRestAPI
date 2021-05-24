import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Administrator, AdministratorAdapter } from '../models/administrator';

@Injectable({
  providedIn: 'root'
})
export class AdministratorService {

  private adminURL = 'http://localhost:5000/api/administrators';

  constructor(
    private http: HttpClient,
    private adapter: AdministratorAdapter) { }

  addAdministrator(email: string, name: string, password: string) {
    return this.http.post(this.adminURL, {
      Email: email,
      Name: name,
      Password: password
    });
  }

  getAdministrators() : Observable<Administrator[]> {
    return this.http.get<Administrator[]>(this.adminURL)
    .pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item)))
    );
  }

  deleteAdministrator(id: number){
    return this.http.delete(`${this.adminURL}/${id}`)
  }
}
