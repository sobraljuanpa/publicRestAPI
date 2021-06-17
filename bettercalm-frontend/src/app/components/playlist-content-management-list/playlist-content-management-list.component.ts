import { Component, OnInit } from '@angular/core';
import { PlayableContent } from 'src/app/models/playableContent';
import { PlayablecontentService } from 'src/app/services/playablecontent.service';
import { ActivatedRoute } from '@angular/router';
import { Playlist } from 'src/app/models/playlist';
import { VideoContent } from 'src/app/models/videoContent';


@Component({
  selector: 'app-playlist-content-management-list',
  templateUrl: './playlist-content-management-list.component.html',
  styleUrls: ['./playlist-content-management-list.component.css']
})
export class PlaylistContentManagementListComponent implements OnInit {

  contents!: PlayableContent[];
  videos!: VideoContent[];
  playlist!: Playlist;

  constructor(
    private contentService: PlayablecontentService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const id= Number(this.route.snapshot.paramMap.get('id'));
    this.getContents(id);
    this.getVideos(id);
    this.getPlaylist(id);
  }

  getContents(id: number) {
    this.contentService.getPlaylistContents(id)
    .subscribe(contents => this.contents = contents);
  }

  getVideos(id: number) {
    this.contentService.getPlaylistVideos(id)
    .subscribe(videos => this.videos = videos);
  }

  getPlaylist(id: number) {
    this.contentService.getPlaylist(id)
    .subscribe(playlist => this.playlist = playlist);
  }

  RemoveContent(id: number) {
    this.contentService.removeContentFromPlaylist(this.playlist.id, id)
    .subscribe(response => window.location.reload());
    console.log("Removing content with id " + id + "from playlist " + this.playlist.name)
  }

  RemoveVideo(id: number) {
    this.contentService.removeVideoFromPlaylist(this.playlist.id, id)
    .subscribe(response => window.location.reload());
    console.log("Removing video with id " + id + "from playlist " + this.playlist.name)
  }

}
