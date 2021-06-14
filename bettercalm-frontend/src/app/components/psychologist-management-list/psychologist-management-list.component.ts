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

  GetSpecialtyName(id: number) {
    switch (id) {
      case 1:
        return "Depresión";
      case 2:
        return "Estrés"
      case 3:
        return "Ansiedad";
      case 4:
        return "Autoestima";
      case 5:
        return "Enojo";
      case 6:
        return "Relaciones";
      case 7:
        return "Duelo";
      case 8:
        return "Y más";
      default:
        return "";
    }
  }


}
