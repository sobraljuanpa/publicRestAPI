import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class Consultation {
    constructor(
        public id: number,
        public patientName: string,
        public patientBirthDate: Date,
        public patientEmail: string,
        public patientPhone: string,
        public problemId: number,
        public psychologistId: number,
        public isRemote: boolean,
        public address: string,
        public date: number,
        public duration: number,
        public bonus: number
    ) { }
}

@Injectable({
    providedIn: 'root'
})
export class ConsultationAdapter implements Adapter<Consultation> {
    adapt(item: any): Consultation {
        return new Consultation(
            item.id,
            item.patientName,
            item.patientBirthDate,
            item.patientEmail,
            item.patientPhone,
            item.problemId,
            item.psychologistId,
            item.isRemote,
            item.address,
            item.date,
            item.duration,
            item.bonus
        )
    }
}