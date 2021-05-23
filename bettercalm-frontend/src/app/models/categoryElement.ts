import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class CategoryElement {
  constructor(
    public id: number,
    public name: string,
    public imageURL: string,
    public contentURL?: string,
    public author?: string,
    public duration?: number,
    public description?: string
  ){}
}

@Injectable({
  providedIn: 'root'
})
export class CategoryElementAdapter implements Adapter<CategoryElement> {
  adapt(item: any): CategoryElement {
    return new CategoryElement(
      item.id,
      item.name,
      item.imageURL,
      item.contentURL,
      item.author,
      item.duration,
      item.description
    );
  }
}
