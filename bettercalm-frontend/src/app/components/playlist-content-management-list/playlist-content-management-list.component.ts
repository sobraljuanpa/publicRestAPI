import { Component, OnInit } from '@angular/core';
import { PlayableContent } from 'src/app/models/playableContent';
import { PlayablecontentService } from 'src/app/services/playablecontent.service';
import { ActivatedRoute } from '@angular/router';
import { Playlist } from 'src/app/models/playlist';


@Component({
  selector: 'app-playlist-content-management-list',
  templateUrl: './playlist-content-management-list.component.html',
  styleUrls: ['./playlist-content-management-list.component.css']
})
export class PlaylistContentManagementListComponent implements OnInit {

  contents!: PlayableContent[];
  playlist!: Playlist;

  constructor(
    private contentService: PlayablecontentService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const id= Number(this.route.snapshot.paramMap.get('id'));
    this.getContents(id);
    this.getPlaylist(id);
  }

  getContents(id: number) {
    this.contentService.getPlaylistContents(id)
    .subscribe(contents => this.contents = contents);
  }

  getPlaylist(id: number) {
    this.contentService.getPlaylist(id)
    .subscribe(playlist => this.playlist = playlist);
  }

  Remove(id: number) {
    this.contentService.removeContentFromPlaylist(this.playlist.id, id)
    .subscribe(response => window.location.reload());
    console.log("Removing content with id " + id + "from playlist " + this.playlist.name)
  }

}
