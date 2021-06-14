import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

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
  psychologistForm = new FormGroup({
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
    private location: Location,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.psychologistService.getPsychologist(id).subscribe(
      psychologist => {
        this.psychologist = psychologist;
        this.psychologistForm.controls.name.setValue(psychologist.name);
        this.psychologistForm.controls.surname.setValue(psychologist.surname);
        this.psychologistForm.controls.address.setValue(psychologist.address);
        this.psychologistForm.controls.schedule.setValue('Schedule');
        this.psychologistForm.controls.expertiseId1.setValue(psychologist.expertiseId1);
        this.psychologistForm.controls.expertiseId2.setValue(psychologist.expertiseId2);
        this.psychologistForm.controls.expertiseId3.setValue(psychologist.expertiseId3);
      }
    );
  }



}
