import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from "@angular/router";

import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
    if(this.authenticationService.isAuthenticated()){
      this.router.navigateByUrl("");
    }
  }

  onSubmit(): void {
    this.authenticationService.login(this.loginForm.controls.email.value, this.loginForm.controls.password.value).subscribe(
      res => {
        console.log(res);
        this.authenticationService.setToken((res as any).token);
      },
      err => {
        console.log("incorrect credentials");
      },
      () => location.reload()
    );
    console.log(`mail ${this.loginForm.controls.email.value} password ${this.loginForm.controls.password.value}`)
  }

}
