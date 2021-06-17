import { Component, OnInit } from '@angular/core';
import { Playlist } from 'src/app/models/playlist';
import { PlayablecontentService } from 'src/app/services/playablecontent.service';

@Component({
  selector: 'app-playlist-management-list',
  templateUrl: './playlist-management-list.component.html',
  styleUrls: ['./playlist-management-list.component.css']
})
export class PlaylistManagementListComponent implements OnInit {

  playlists!: Playlist[];

  constructor(private contentService: PlayablecontentService) { }

  ngOnInit(): void {
    this.getPlaylists();
  }

  getPlaylists() {
    this.contentService.getPlaylists()
    .subscribe(playlists => this.playlists = playlists);
  }

  Delete(id: number) {
    this.contentService.deletePlaylist(id)
    .subscribe(response => window.location.reload());
  }

}
