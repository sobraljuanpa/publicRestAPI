import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Category, CategoryAdapter } from "../models/category";
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CategoryElement, CategoryElementAdapter } from '../models/categoryElement';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private categoriesURL = 'http://localhost:5000/api/categories';

  constructor(
    private http: HttpClient,
    private adapter: CategoryAdapter,
    private elementAdapter: CategoryElementAdapter) { }

  getCategories() : Observable<Category[]> {
    return this.http.get<Category[]>(this.categoriesURL)
    .pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item)))
    );
  }

  getCategoryElements(categoryId : number) : Observable<CategoryElement[]> {
    return this.http.get<CategoryElement[]>(`${this.categoriesURL}/${categoryId}`)
    .pipe(
      map((data: any[]) => data.map(item => this.elementAdapter.adapt(item)))
    );
  }
}
