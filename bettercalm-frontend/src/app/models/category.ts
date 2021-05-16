import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class Category {
    constructor(
        public id: number,
        public name: string
    ){}
}

@Injectable({
    providedIn: 'root'
})
export class CategoryAdapter implements Adapter<Category> {
    adapt(item: any): Category {
        return new Category (
            item.Id,
            item.Name
        );
    }
}
