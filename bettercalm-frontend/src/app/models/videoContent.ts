import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class VideoContent {
  constructor(
    public id: number,
    public name: string,
    public author: string,
    public duration: number,
    public videoURL: string,
    public categoryId?: number
  ){}
}

@Injectable({
  providedIn: 'root'
})
export class VideoAdapter implements Adapter<VideoContent> {
  adapt(item: any): VideoContent {
    return new VideoContent(
      item.id,
      item.name,
      item.author,
      item.duration,
      item.videoURL,
      item.categoryId
    );
  }
}
