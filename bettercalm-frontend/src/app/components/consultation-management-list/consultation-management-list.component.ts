import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Location } from '@angular/common';

import { Consultation } from 'src/app/models/consultation';
import { ConsultationService } from 'src/app/services/consultation.service';

@Component({
  selector: 'app-consultation-management-list',
  templateUrl: './consultation-management-list.component.html',
  styleUrls: ['./consultation-management-list.component.css']
})
export class ConsultationManagementListComponent implements OnInit {

  consultations!: Consultation[];

  constructor(
    private ConsultationService: ConsultationService) { }

  ngOnInit(): void {
    this.getConsultations();
  }

  getConsultations() {
    this.ConsultationService.getConsultations()
      .subscribe(consultations => this.consultations = consultations);

  }
}
