import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Psychologist, PsychologistAdapter } from '../models/psychologist';


@Injectable({
  providedIn: 'root'
})
export class PsychologistService {

  private psychologistURL = "http://localhost:5000/api/psychologists";

  constructor(
    private http: HttpClient,
    private psychologistAdapter: PsychologistAdapter) { }

  addPsychologist(
    name: string,
    surname: string,
    isRemote: boolean,
    address: string,
    activeYears: number,
    scheduleId: number,
    expertiseId1: number,
    expertiseId2: number,
    expertiseId3: number) {
    return this.http.post(this.psychologistURL, {
      PsychologistName: name,
      PsychologistSurname: surname,
      IsRemote: isRemote,
      Address: address,
      ActiveYears: activeYears,
      scheduleId: scheduleId,
      ExpertiseId1: expertiseId1,
      ExpertiseId2: expertiseId2,
      ExpertiseId3: expertiseId3
    })
  }
}


