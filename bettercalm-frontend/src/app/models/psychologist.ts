import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class Psychologist {
    constructor(
        public id: number,
        public psychologistName: string,
        public psychologistSurname: string,
        public isRemote: boolean,
        public address: string,
        public activeYears: number,
        public fee: number,
        public expertiseId1: number,
        public expertiseId2: number,
        public expertiseId3: number,
        public scheduleId: number
    ) { }
}

@Injectable({
    providedIn: 'root'
})
export class PsychologistAdapter implements Adapter<Psychologist> {
    adapt(item: any): Psychologist {
        return new Psychologist(
            item.id,
            item.psychologistName,
            item.psychologistSurname,
            item.isRemote,
            item.address,
            item.activeYears,
            item.fee,
            item.expertiseId1,
            item.expertiseId2,
            item.expertiseId3,
            item.scheduleId
        )
    }
}
