import { Injectable } from '@angular/core';

import { Category, CategoryAdapter } from "../models/category";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  categories = [
    {id: 1, name: "Algo"},
    {id: 2, name: "Otra"},
    {id: 3, name: "Distinta"},
    {id: 4, name: "Alternativa"}
  ]

  constructor() { }

  getCategories() : Category[] {
    return this.categories;
  }
}
