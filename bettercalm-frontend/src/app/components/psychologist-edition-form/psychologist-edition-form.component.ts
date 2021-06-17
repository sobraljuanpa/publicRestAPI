import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Location } from '@angular/common';

import { Psychologist } from 'src/app/models/psychologist';
import { PsychologistService } from 'src/app/services/psychologist.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-psychologist-edition-form',
  templateUrl: './psychologist-edition-form.component.html',
  styleUrls: ['./psychologist-edition-form.component.css']
})
export class PsychologistEditionFormComponent implements OnInit {

  psychologist?: Psychologist;
  scheduleId?: number;
  psychologistForm = new FormGroup({
    psychologistName: new FormControl(''),
    psychologistSurname: new FormControl(''),
    isRemote: new FormControl(''),
    address: new FormControl(''),
    activeYears: new FormControl(''),
    fee: new FormControl(''),
    expertiseId1: new FormControl(''),
    expertiseId2: new FormControl(''),
    expertiseId3: new FormControl('')
  });

  constructor(
    private psychologistService: PsychologistService,
    private location: Location,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.psychologistService.getPsychologist(id).subscribe(
      psychologist => {
        this.psychologist = psychologist;
        this.psychologistForm.controls.psychologistName.setValue(psychologist.psychologistName);
        this.psychologistForm.controls.psychologistSurname.setValue(psychologist.psychologistSurname);
        this.psychologistForm.controls.isRemote.setValue(psychologist.isRemote);
        this.psychologistForm.controls.address.setValue(psychologist.address);
        this.psychologistForm.controls.activeYears.setValue(psychologist.activeYears);
        this.psychologistForm.controls.fee.setValue(psychologist.fee);
        this.psychologistForm.controls.expertiseId1.setValue(psychologist.expertiseId1);
        this.psychologistForm.controls.expertiseId2.setValue(psychologist.expertiseId2);
        this.psychologistForm.controls.expertiseId3.setValue(psychologist.expertiseId3);
        this.scheduleId = psychologist.scheduleId;
      }
    );
  }

  onSubmit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.psychologistService.updatePsychologist(
      id,
      this.psychologistForm.controls.psychologistName.value,
      this.psychologistForm.controls.psychologistSurname.value,
      this.psychologistForm.controls.isRemote.value,
      this.psychologistForm.controls.address.value,
      this.psychologistForm.controls.activeYears.value,
      this.psychologistForm.controls.fee.value,
      this.psychologistForm.controls.expertiseId1.value,
      this.psychologistForm.controls.expertiseId2.value,
      this.psychologistForm.controls.expertiseId3.value,
      this.scheduleId!
    ).subscribe(
      res => { this.location.back() }
    )
  }

  goBack() {
    this.location.back();
  }
}
