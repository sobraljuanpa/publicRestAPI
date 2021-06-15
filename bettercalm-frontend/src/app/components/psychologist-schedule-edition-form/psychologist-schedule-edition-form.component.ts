import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PsychologistService } from 'src/app/services/psychologist.service';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-psychologist-schedule-edition-form',
  templateUrl: './psychologist-schedule-edition-form.component.html',
  styleUrls: ['./psychologist-schedule-edition-form.component.css']
})
export class PsychologistScheduleEditionFormComponent implements OnInit {

  scheduleForm = new FormGroup({
    mondaySchedule: new FormControl(''),
    tuesdaySchedule: new FormControl(''),
    wednesdaySchedule: new FormControl(''),
    thursdaySchedule: new FormControl(''),
    fridaySchedule: new FormControl('')
  });

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private psychologistService: PsychologistService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.psychologistService.getSchedule(id).subscribe(
      res => {
        console.log(res);
        this.scheduleForm.controls.mondaySchedule.setValue(res.mon);
        this.scheduleForm.controls.tuesdaySchedule.setValue(res.tue);
        this.scheduleForm.controls.wednesdaySchedule.setValue(res.wed);
        this.scheduleForm.controls.thursdaySchedule.setValue(res.thu);
        this.scheduleForm.controls.fridaySchedule.setValue(res.fri);
      }
    )
  }

  goBack() {
    this.location.back();
  }

  onSubmit() {

  }

}
