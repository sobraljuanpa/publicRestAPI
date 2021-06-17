import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ImportationService } from '../../services/importation.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-import-playablecontent-form',
  templateUrl: './import-playablecontent-form.component.html',
  styleUrls: ['./import-playablecontent-form.component.css']
})
export class ImportPlayablecontentFormComponent implements OnInit {

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

  importPlayableContent() {
    this.array = [this.path];
    this.importationService.addImportation(this.selectedType, this.array).subscribe(
      res => {
        console.log(res);
        this.toastr.success("Importation successful!")
        this.location.back();
      },
      err => {
        console.log(err);
        this.toastr.error("Importation failed!")
      }
    )
  }

}
