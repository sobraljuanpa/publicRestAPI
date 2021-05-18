import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AdministratorService } from '../../services/administrator.service';

@Component({
  selector: 'app-add-administrator-form',
  templateUrl: './add-administrator-form.component.html',
  styleUrls: ['./add-administrator-form.component.css']
})
export class AddAdministratorFormComponent implements OnInit {

  adminForm = new FormGroup({
    email: new FormControl(''),
    name: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private administratorService: AdministratorService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.administratorService.addAdministrator(
      this.adminForm.controls.email.value,
      this.adminForm.controls.name.value,
      this.adminForm.controls.password.value
    ).subscribe(
      res => {
        console.log(res);
      },
      err => {
        if(err.status == 401){
          console.log("Incorrect credentials");
        }
        else{
          console.log(err);
        }
      }
    )
  }

}
