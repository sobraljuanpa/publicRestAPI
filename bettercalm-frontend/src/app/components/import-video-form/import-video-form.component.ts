import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ImportationService } from '../../services/importation.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-import-video-form',
  templateUrl: './import-video-form.component.html',
  styleUrls: ['./import-video-form.component.css']
})
export class ImportVideoFormComponent implements OnInit {

  selectedType!: string;
  path!: object;
  array!: object[];

  constructor(
    private importationService: ImportationService,
    private location: Location,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
  }

  importVideo() {
    this.array = [this.path];
    this.importationService.addImportation(this.selectedType, this.array).subscribe(
      res => {
        this.toastr.success("Importation successful!")
        this.location.back();
      },
      err => {
        this.toastr.error("Importation failed!")
      }
    )
  }
}
