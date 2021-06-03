import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PlayableContent, PlayableContentAdapter } from '../models/playableContent';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PlayablecontentService {

  private playablecontentURL = "http://localhost:5000/api/playablecontents";
  private playlistURL = "http://localhost:5000/api/playlists";

  constructor(
    private http: HttpClient,
    private adapter: PlayableContentAdapter) { }

  addContent(
    name: string,
    author: string,
    categoryId: number,
    duration: number,
    imageURL: string,
    contentURL: string){
      return this.http.post(this.playablecontentURL, {
        Name: name,
        Author: author,
        CategoryId: categoryId,
        Duration: duration,
        ImageURL: imageURL,
        ContentURL: contentURL
      })
  }

  addPlaylist(
    name: string,
    description: string,
    categoryId: number,
    imageURL: string){
      return this.http.post(this.playlistURL, {
        Name: name,
        Description: description,
        CategoryId: categoryId,
        ImageURL: imageURL
      })
  }

  getContents() : Observable<PlayableContent[]> {
    return this.http.get<PlayableContent[]>(this.playablecontentURL)
    .pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item)))
    )
  }

  deleteContent(id: number){
    return this.http.delete(`${this.playablecontentURL}/${id}`)
  }


}
