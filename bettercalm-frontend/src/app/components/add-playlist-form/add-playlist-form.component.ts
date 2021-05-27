import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { PlayablecontentService } from 'src/app/services/playablecontent.service';

@Component({
  selector: 'app-add-playlist-form',
  templateUrl: './add-playlist-form.component.html',
  styleUrls: ['./add-playlist-form.component.css']
})
export class AddPlaylistFormComponent implements OnInit {

  contentForm = new FormGroup({
    name: new FormControl(''),
    category: new FormControl(''),
    imageURL: new FormControl(''),
    description: new FormControl('')
  });

  constructor(private playablecontentService: PlayablecontentService) { }

  ngOnInit(): void {
  }

  onSubmit() {

  }

}
