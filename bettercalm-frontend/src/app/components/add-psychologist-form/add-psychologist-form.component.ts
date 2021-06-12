import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { PsychologistService } from 'src/app/services/psychologist.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-psychologist-form',
  templateUrl: './add-psychologist-form.component.html',
  styleUrls: ['./add-psychologist-form.component.css']
})
export class AddPsychologistFormComponent implements OnInit {

  contentForm = new FormGroup({
    psychologistName: new FormControl(''),
    psychologistSurname: new FormControl(''),
    isRemote: new FormControl(''),
    address: new FormControl(''),
    activeYears: new FormControl(''),
    expertise1: new FormControl(''),
    expertise2: new FormControl(''),
    expertise3: new FormControl(''),
  });
  constructor(private psychologistService: PsychologistService,
    private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.psychologistService.addPsychologist(
      this.contentForm.controls.name.value,
      this.contentForm.controls.surname.value,
      this.contentForm.controls.isRemote.value,
      this.contentForm.controls.address.value,
      this.contentForm.controls.activeYears.value,
      this.contentForm.controls.expertiseId1.value,
      this.contentForm.controls.expertiseId2.value,
      this.contentForm.controls.expertiseId3.value,
    ).subscribe(
      res => {
        this.router.navigateByUrl("/psychologists");
      },
      err => {
        if (err.status == 401) {
          console.log("Incorrect credentials");
        }
        else {
          console.log(err);
        }
      }
    )
  }

}
