import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Psychologist, PsychologistAdapter } from '../models/psychologist';
import { Schedule, ScheduleAdapter } from '../models/schedule';
import { GlobalVariables } from '../globals';


@Injectable({
  providedIn: 'root'
})
export class PsychologistService {

  private psychologistURL = `${GlobalVariables.BASE_API_URL}/psychologists`;
  private schedulesURL = `${GlobalVariables.BASE_API_URL}/psychologists/schedules`;

  constructor(
    private http: HttpClient,
    private psychologistAdapter: PsychologistAdapter,
    private scheduleAdapter: ScheduleAdapter) { }

  addPsychologist(
    name: string,
    surname: string,
    isRemote: boolean,
    address: string,
    activeYears: number,
    fee: number,
    expertiseId1: number,
    expertiseId2: number,
    expertiseId3: number) {
    return this.http.post(this.psychologistURL, {
      PsychologistName: name,
      PsychologistSurname: surname,
      IsRemote: isRemote,
      Address: address,
      ActiveYears: activeYears,
      Fee: fee,
      ScheduleId: 0,
      ExpertiseId1: expertiseId1,
      ExpertiseId2: expertiseId2,
      ExpertiseId3: expertiseId3
    })
  }

  getPsychologists(): Observable<Psychologist[]> {
    return this.http.get<Psychologist[]>(this.psychologistURL)
      .pipe(
        map((data: any[]) => data.map(item => this.psychologistAdapter.adapt(item)))
      )
  }

  getPsychologist(id: number): Observable<Psychologist> {
    return this.http.get<Psychologist>(`${this.psychologistURL}/${id}`)
      .pipe(
        map((data: any) => data = this.psychologistAdapter.adapt(data))
      );
  }

  getSchedule(id: number): Observable<Schedule> {
    return this.http.get<Schedule>(`${this.schedulesURL}/${id}`)
      .pipe(
        map((data: any) => data = this.scheduleAdapter.adapt(data))
      );
  }

  updateSchedule(
    id: number,
    mon: number,
    tue: number,
    wed: number,
    thu: number,
    fri: number){
    return this.http.put(`${this.schedulesURL}/${id}`, {
      Id: id,
      MondayConsultations: mon,
      TuesdayConsultations: tue,
      WednesdayConsultations: wed,
      ThursdayConsultations: thu,
      FridayConsultations: fri
    })
  }


  deletePsychologist(id: number) {
    return this.http.delete(`${this.psychologistURL}/${id}`)
  }

  updatePsychologist(
    id: number,
    name: string,
    surname: string,
    isRemote: boolean,
    address: string,
    activeYears: number,
    fee: number,
    expertiseId1: number,
    expertiseId2: number,
    expertiseId3: number,
    scheduleId: number) {
    return this.http.put(`${this.psychologistURL}/${id}`, {
      Id: id,
      PsychologistName: name,
      PsychologistSurname: surname,
      IsRemote: isRemote,
      Address: address,
      ActiveYears: activeYears,
      Fee: fee,
      ExpertiseId1: expertiseId1,
      ExpertiseId2: expertiseId2,
      ExpertiseId3: expertiseId3,
      ScheduleId: scheduleId
    })
  }

}


