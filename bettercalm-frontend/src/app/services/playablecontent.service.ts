import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PlayablecontentService {

  private playablecontentURL = "http://localhost:5000/api/playablecontents";

  constructor(private http: HttpClient) { }

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
}
