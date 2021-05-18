import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

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

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit() {

  }

}
