import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PlayableContent, PlayableContentAdapter } from '../models/playableContent';
import { Playlist, PlaylistAdapter } from '../models/playlist';
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
    private adapter: PlayableContentAdapter,
    private playlistAdapter: PlaylistAdapter) { }

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

  updateContent(
    id: string,
    name: string,
    author: string,
    categoryId: number,
    duration: number,
    imageURL: string,
    contentURL: string){
      return this.http.put(`${this.playablecontentURL}/${id}`, {
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

  addContentToPlaylist(playlistId: number, contentId: number) {
    return this.http.post(`${this.playlistURL}/${playlistId}/contents?contentId=${contentId}`, {});
  }

  getContent(id: number) : Observable<PlayableContent> {
    return this.http.get<PlayableContent>(`${this.playablecontentURL}/${id}`)
    .pipe(
      map((data: any) => data = this.adapter.adapt(data))
    )
  }

  getContents() : Observable<PlayableContent[]> {
    return this.http.get<PlayableContent[]>(this.playablecontentURL)
    .pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item)))
    )
  }

  getPlaylists() : Observable<Playlist[]> {
    return this.http.get<Playlist[]>(this.playlistURL)
    .pipe(
      map((data: any[]) => data.map(item => this.playlistAdapter.adapt(item)))
    )
  }

  deleteContent(id: number){
    return this.http.delete(`${this.playablecontentURL}/${id}`)
  }

  deletePlaylist(id: number){
    return this.http.delete(`${this.playlistURL}/${id}`)
  }


}
