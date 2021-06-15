import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Consultation, ConsultationAdapter } from '../models/consultation';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConsultationService {

  private consultationURL = "http://localhost:5000/api/consultations";

  constructor(
    private http: HttpClient,
    private consultationAdapter: ConsultationAdapter) { }

  addConsultation(
    patientName: string,
    patientBirthDate: string,
    patientEmail: string,
    patientPhone: string,
    problemId: number,
    psychologistId: number,
    address: string,
    date: number,
    duration: number,
    bonus: number) {
    return this.http.post(this.consultationURL, {
      PatientName: patientName,
      PatientBirthDate: patientBirthDate,
      PatientEmail: patientEmail,
      PatientPhone: patientPhone,
      ProblemId: problemId,
      PsychologistId: psychologistId,
      Address: address,
      Date: date,
      Duration: duration,
      Bonus: bonus
    })
  }

  getConsultations(): Observable<Consultation[]> {
    return this.http.get<Consultation[]>(this.consultationURL)
      .pipe(
        map((data: any[]) => data.map(item => this.consultationAdapter.adapt(item)))
      )
  }


  getConsultation(id: number): Observable<Consultation> {
    return this.http.get<Consultation>(`${this.consultationURL}/${id}`)
      .pipe(
        map((data: any) => data = this.consultationAdapter.adapt(data))
      );
  }
}
