import { Component, OnInit } from '@angular/core';
import { PlayableContent } from 'src/app/models/playableContent';
import { Playlist } from 'src/app/models/playlist';
import { PlayablecontentService } from 'src/app/services/playablecontent.service';

@Component({
  selector: 'app-playablecontent-management-list',
  templateUrl: './playablecontent-management-list.component.html',
  styleUrls: ['./playablecontent-management-list.component.css']
})
export class PlayablecontentManagementListComponent implements OnInit {

  contents!: PlayableContent[];
  playlists!: Playlist[];
  selectedPlaylist!: Playlist;

  constructor(private contentService: PlayablecontentService) { }

  ngOnInit(): void {
    this.getContents();
    this.getPlaylists();
  }

  getPlaylists() {
    this.contentService.getPlaylists()
    .subscribe(playlists => this.playlists = playlists)
  }

  getContents() {
    this.contentService.getContents()
    .subscribe(contents => this.contents = contents);
  }

  Delete(id: number) {
    this.contentService.deleteContent(id)
    .subscribe(response => window.location.reload())
  }

  AddToPlaylist(playlistId: number, contentId: number) {
    this.contentService.addContentToPlaylist(playlistId, contentId)
    .subscribe(response => console.log(response));
  }

}
