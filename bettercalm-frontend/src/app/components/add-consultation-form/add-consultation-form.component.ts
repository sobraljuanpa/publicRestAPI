import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConsultationService } from 'src/app/services/consultation.service';

@Component({
  selector: 'app-add-consultation-form',
  templateUrl: './add-consultation-form.component.html',
  styleUrls: ['./add-consultation-form.component.css']
})
export class AddConsultationFormComponent implements OnInit {

  consultationForm = new FormGroup({
    patientName: new FormControl(''),
    patientBirthDate: new FormControl(''),
    patientEmail: new FormControl(''),
    patientPhone: new FormControl(''),
    problemId: new FormControl(''),
    isRemote: new FormControl(''),
    address: new FormControl(''),
    date: new FormControl(''),
    duration: new FormControl(''),
    bonus: new FormControl(''),
  });

  constructor(
    private consultationService: ConsultationService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.consultationService.addConsultation(
      this.consultationForm.controls.patientName.value,
      this.consultationForm.controls.patientBirthDate.value,
      this.consultationForm.controls.patientEmail.value,
      this.consultationForm.controls.patientPhone.value,
      this.consultationForm.controls.problemId.value,
      this.consultationForm.controls.isRemote.value,
      this.consultationForm.controls.address.value,
      this.consultationForm.controls.date.value,
      this.consultationForm.controls.duration.value,
      this.consultationForm.controls.bonus.value

    ).subscribe(
      res => {
        this.router.navigateByUrl("/consultations");
      },
      err => {
        if (err.message == undefined) {
          this.toastr.error("Please double check all parameters are properly set", "Error adding consultation");
        } else {
          this.toastr.error(err.message, "Error adding consultation");
        }
      }
    )
  }

}
