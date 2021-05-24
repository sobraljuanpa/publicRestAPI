import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { Administrator } from 'src/app/models/administrator';
import { AdministratorService } from 'src/app/services/administrator.service';

@Component({
  selector: 'app-administrator-edition-form',
  templateUrl: './administrator-edition-form.component.html',
  styleUrls: ['./administrator-edition-form.component.css']
})
export class AdministratorEditionFormComponent implements OnInit {

  administrator?: Administrator;
  adminForm = new FormGroup({
    email: new FormControl(''),
    name: new FormControl(''),
    password: new FormControl(''),
  });


  constructor(
    private administratorService: AdministratorService,
    private location: Location,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const id= Number(this.route.snapshot.paramMap.get('id'));
    this.administratorService.getAdministrator( id ).subscribe(
      administrator => {
        this.administrator = administrator;
        this.adminForm.controls.name.setValue(administrator.name);
        this.adminForm.controls.email.setValue(administrator.email);
        this.adminForm.controls.password.setValue(administrator.password);
      }
    );
  }

  onSubmit() {
    console.log("submitteado");
    console.log(this.adminForm.controls.email.value);
    console.log(this.adminForm.controls.name.value);
    console.log(this.adminForm.controls.password.value);
  }

  goBack() {
    this.location.back();
  }

}
