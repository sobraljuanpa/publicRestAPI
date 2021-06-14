import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { PsychologistService } from 'src/app/services/psychologist.service';


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
    expertiseId1: new FormControl(''),
    expertiseId2: new FormControl(''),
    expertiseId3: new FormControl('')
  });
  constructor(
    private psychologistService: PsychologistService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    debugger;
    this.psychologistService.addPsychologist(
      this.contentForm.controls.psychologistName.value,
      this.contentForm.controls.psychologistSurname.value,
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
        if (err.message == undefined) {
          this.toastr.error("Please double check all parameters are properly set",
            "Error adding psychologist");
        } else {
          this.toastr.error(err.message, "Error adding psychologist");
        }
      }
    )
  }

}
