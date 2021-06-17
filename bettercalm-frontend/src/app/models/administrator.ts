import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class Administrator {
    constructor(
        public id: number,
        public email: string,
        public name: string,
        public password: string
    ){}
}

@Injectable({
    providedIn: 'root'
})
export class AdministratorAdapter implements Adapter<Administrator> {
    adapt(item: any): Administrator {
        return new Administrator (
            item.id,
            item.email,
            item.name,
            item.password
        );
    }
}
