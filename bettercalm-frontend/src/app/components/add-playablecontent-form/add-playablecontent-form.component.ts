import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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

  constructor(private playablecontentService: PlayablecontentService) { }

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
        console.log(res);
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
