import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr'

import { PlayablecontentService } from 'src/app/services/playablecontent.service';

@Component({
  selector: 'app-add-video-form',
  templateUrl: './add-video-form.component.html',
  styleUrls: ['./add-video-form.component.css']
})
export class AddVideoFormComponent implements OnInit {

  videoForm = new FormGroup({
    name: new FormControl(''),
    category: new FormControl(''),
    author: new FormControl(''),
    duration: new FormControl(''),
    videoURL: new FormControl('')
  });

  constructor(
    private playablecontentService: PlayablecontentService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.playablecontentService.addVideo(
      this.videoForm.controls.name.value,
      this.videoForm.controls.author.value,
      this.videoForm.controls.category.value,
      this.videoForm.controls.duration.value,
      this.videoForm.controls.videoURL.value
    ).subscribe(
      res => {
        this.router.navigateByUrl("/videos");
      },
      err => {
        if(err.message == undefined){
          this.toastr.error("Please double check all parameters are properly set", "Error adding video");
        } else {
          this.toastr.error(err.message, "Error adding video");
        }
      }
    )
  }

}
