import { Component, OnInit } from '@angular/core';
import { Psychologist } from 'src/app/models/psychologist';
import { PsychologistService } from 'src/app/services/psychologist.service';

@Component({
  selector: 'app-psychologist-management-list',
  templateUrl: './psychologist-management-list.component.html',
  styleUrls: ['./psychologist-management-list.component.css']
})
export class PsychologistManagementListComponent implements OnInit {

  psychologists!: Psychologist[];

  constructor(private psychologistService: PsychologistService,) { }

  ngOnInit(): void {
    this.getPsychologists();
  }

  getPsychologists() {
    this.psychologistService.getPsychologists()
      .subscribe(psychologists => this.psychologists = psychologists);
  }

  Delete(id: number) {
    this.psychologistService.deletePsychologist(id)
      .subscribe(response => window.location.reload())
  }


}
