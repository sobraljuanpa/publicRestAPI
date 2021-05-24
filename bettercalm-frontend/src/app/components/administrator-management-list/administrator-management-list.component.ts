import { Component, OnInit } from '@angular/core';
import { Administrator } from 'src/app/models/administrator';
import { AdministratorService } from 'src/app/services/administrator.service';

@Component({
  selector: 'app-administrator-management-list',
  templateUrl: './administrator-management-list.component.html',
  styleUrls: ['./administrator-management-list.component.css']
})
export class AdministratorManagementListComponent implements OnInit {

  administrators!: Administrator[];

  constructor(private administratorService: AdministratorService) { }

  ngOnInit(): void {
    this.getAdministrators();
  }

  getAdministrators() {
    this.administratorService.getAdministrators()
    .subscribe(administrators => this.administrators = administrators);
  }

  Delete(id: number) {
    console.log(id);
  }

}
