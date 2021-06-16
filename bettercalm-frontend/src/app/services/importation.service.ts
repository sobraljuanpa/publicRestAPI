import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from "@angular/common/http";
import { GlobalVariables } from '../globals';


@Injectable({
  providedIn: 'root'
})
export class ImportationService {

  private playablecontentURL = `${GlobalVariables.BASE_API_URL}/playablecontents`;
  private videocontentURL = `${GlobalVariables.BASE_API_URL}/videos`;
  private playlistURL = `${GlobalVariables.BASE_API_URL}/playlists`;

  constructor(private http: HttpClient) { }

  addPlayableContentImportation(type: string, path: string[]) {
    debugger;
    return this.http.post(`${this.playablecontentURL}/${type}`, path)
  }

  addPlaylistImportation(type: string, path: string[]) {
    debugger;
    return this.http.post(`${this.playlistURL}/${type}`, path)
  }

  addVideoImportation(type: string, path: string[]) {
    debugger;
    return this.http.post(`${this.videocontentURL}/${type}`, path)
  }
}
