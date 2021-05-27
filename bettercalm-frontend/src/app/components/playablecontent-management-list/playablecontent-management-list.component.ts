import { Component, OnInit } from '@angular/core';
import { PlayableContent } from 'src/app/models/playableContent';
import { PlayablecontentService } from 'src/app/services/playablecontent.service';

@Component({
  selector: 'app-playablecontent-management-list',
  templateUrl: './playablecontent-management-list.component.html',
  styleUrls: ['./playablecontent-management-list.component.css']
})
export class PlayablecontentManagementListComponent implements OnInit {

  contents!: PlayableContent[];

  constructor(private contentService: PlayablecontentService) { }

  ngOnInit(): void {
    this.getContents();
  }

  getContents() {
    this.contentService.getContents()
    .subscribe(contents => this.contents = contents);
  }

  Delete(id: number) {
    this.contentService.deleteContent(id)
    .subscribe(response => window.location.reload())
  }

}
