import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class Playlist {
  constructor(
    public id: number,
    public name: string,
    public imageURL: string,
    public description: string
  ){}
}

@Injectable({
  providedIn: 'root'
})
export class PlaylistAdapter implements Adapter<Playlist> {
  adapt(item: any): Playlist {
    return new Playlist(
      item.id,
      item.name,
      item.imageURL,
      item.description
    )
  }
}
