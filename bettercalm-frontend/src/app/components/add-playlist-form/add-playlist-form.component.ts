import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
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

  constructor(
    private playablecontentService: PlayablecontentService,
    private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.playablecontentService.addPlaylist(
      this.contentForm.controls.name.value,
      this.contentForm.controls.description.value,
      this.contentForm.controls.category.value,
      this.contentForm.controls.imageURL.value
    ).subscribe(
      res => {
        this.router.navigateByUrl("/playlists")
        console.log(res)
      },
      err => {
        if(err.status == 401){
          console.log("Incorrect credentials");
        }
        else{
          console.log(err);
        }
      }
    )
  }

}
