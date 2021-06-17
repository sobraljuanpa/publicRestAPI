import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { PlayablecontentService } from 'src/app/services/playablecontent.service';

@Component({
  selector: 'app-add-playablecontent-form',
  templateUrl: './add-playablecontent-form.component.html',
  styleUrls: ['./add-playablecontent-form.component.css']
})
export class AddPlayablecontentFormComponent implements OnInit {

  contentForm = new FormGroup({
    name: new FormControl(''),
    category: new FormControl(''),
    author: new FormControl(''),
    duration: new FormControl(''),
    imageURL: new FormControl(''),
    contentURL: new FormControl(''),
  });

  constructor(
    private playablecontentService: PlayablecontentService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.playablecontentService.addContent(
      this.contentForm.controls.name.value,
      this.contentForm.controls.author.value,
      this.contentForm.controls.category.value,
      this.contentForm.controls.duration.value,
      this.contentForm.controls.imageURL.value,
      this.contentForm.controls.contentURL.value
    ).subscribe(
      res => {
        this.router.navigateByUrl("/playablecontents");
      },
      err => {
        if(err.message == undefined){
          this.toastr.error("Please double check all parameters are properly set", "Error adding content");
        } else {
          this.toastr.error(err.message, "Error adding content");

        }
      }
    )
  }

}
