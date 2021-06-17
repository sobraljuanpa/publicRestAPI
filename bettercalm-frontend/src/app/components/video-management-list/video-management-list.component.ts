import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser'

import { Playlist } from 'src/app/models/playlist';
import { VideoContent } from 'src/app/models/videoContent';
import { PlayablecontentService } from 'src/app/services/playablecontent.service';

@Component({
  selector: 'app-video-management-list',
  templateUrl: './video-management-list.component.html',
  styleUrls: ['./video-management-list.component.css']
})
export class VideoManagementListComponent implements OnInit {

  videos!: VideoContent[];

  constructor(
    private contentService: PlayablecontentService
  ) { }

  ngOnInit(): void {
    this.getVideos();
  }

  getVideos() {
    this.contentService.getVideos()
    .subscribe(videos => this.videos = videos);
  }

}
