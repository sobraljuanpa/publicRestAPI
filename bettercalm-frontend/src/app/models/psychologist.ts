import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class Psychologist {
    constructor(
        id: number,
        name: string,
        surname: string,
        isRemote: boolean,
        address: string,
        activeYears: number,
        expertiseId1: number,
        expertiseId2: number,
        expertiseId3: number,
    ) { }
}

@Injectable({
    providedIn: 'root'
})
export class PsychologistAdapter implements Adapter<Psychologist> {
    adapt(item: any): Psychologist {
        return new Psychologist(
            item.id,
            item.name,
            item.surname,
            item.isRemote,
            item.address,
            item.activeYears,
            item.expertiseId1,
            item.expertiseId2,
            item.expertiseId3,
        )
    }
}
