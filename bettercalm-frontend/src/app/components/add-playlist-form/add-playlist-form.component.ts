import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

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
    private router: Router,
    private toastr: ToastrService) { }

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
        if(err.message == undefined){
          this.toastr.error("Please double check all parameters are properly set", "Error adding playlist");
        } else {
          this.toastr.error(err.message, "Error adding playlist");
        }
      }
    )
  }

}
