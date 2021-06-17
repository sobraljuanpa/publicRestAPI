import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class Schedule {
    constructor(
        public id: number,
        public mon: number,
        public tue: number,
        public wed: number,
        public thu: number,
        public fri: number
    ) { }
}

@Injectable({
    providedIn: 'root'
})
export class ScheduleAdapter implements Adapter<Schedule> {
    adapt(item: any): Schedule {
        return new Schedule(
            item.id,
            item.mondayConsultations,
            item.tuesdayConsultations,
            item.wednesdayConsultations,
            item.thursdayConsultations,
            item.fridayConsultations
        )
    }
}
