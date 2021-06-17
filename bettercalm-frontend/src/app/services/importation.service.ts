import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from "@angular/common/http";
import { GlobalVariables } from '../globals';


@Injectable({
  providedIn: 'root'
})
export class ImportationService {

  private importationURL = `${GlobalVariables.BASE_API_URL}/importations`;

  constructor(private http: HttpClient) { }

  addImportation(type: string, parameters: object[]) {
    debugger;
    return this.http.post(`${this.importationURL}/${type}`, { parameters: parameters })
  }
}
