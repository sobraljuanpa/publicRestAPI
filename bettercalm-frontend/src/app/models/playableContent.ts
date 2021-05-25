import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class PlayableContent {
  constructor(
    public id: number,
    public name: string,
    public imageURL: string,
    public contentURL: string,
    public author: string,
    public duration: number
  ){}
}

@Injectable({
  providedIn: 'root'
})
export class PlayableContentAdapter implements Adapter<PlayableContent> {
  adapt(item: any): PlayableContent {
    return new PlayableContent(
      item.id,
      item.name,
      item.imageURL,
      item.contentURL,
      item.author,
      item.duration
    );
  }
}
