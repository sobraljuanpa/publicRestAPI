import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Playlist } from 'src/app/models/playlist';
import { VideoContent } from 'src/app/models/videoContent';
import { PlayablecontentService } from 'src/app/services/playablecontent.service';

@Component({
  selector: 'app-video-list-element',
  templateUrl: './video-list-element.component.html',
  styleUrls: ['./video-list-element.component.css']
})
export class VideoListElementComponent implements OnInit {

  @Input() video?: VideoContent;

  playlists!: Playlist[];
  selectedPlaylist!: Playlist;
  videoLink!: SafeResourceUrl;

  constructor(
    private sanitizer: DomSanitizer,
    private contentService: PlayablecontentService
  ) { }

  ngOnInit(): void {
    this.getPlaylists();
    this.videoLink = this.sanitizer.bypassSecurityTrustResourceUrl(this.video?.videoURL!!);
  }

  getPlaylists() {
    this.contentService.getPlaylists()
    .subscribe(playlists => this.playlists = playlists)
  }


  Delete(id: number) {
    this.contentService.deleteVideo(id)
    .subscribe(response => window.location.reload())
  }

  AddToPlaylist(playlistId: number, videoId: number) {
    this.contentService.addVideoToPlaylist(playlistId, videoId)
    .subscribe(response => console.log(response));
  }

}
