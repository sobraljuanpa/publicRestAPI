import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ImportationService } from '../../services/importation.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-import-playlist-form',
  templateUrl: './import-playlist-form.component.html',
  styleUrls: ['./import-playlist-form.component.css']
})
export class ImportPlaylistFormComponent implements OnInit {

  selectedType!: string;
  path!: string;
  array!: string[];
  typeImport: string[] = ['Xml', 'Json'];

  constructor(
    private importationService: ImportationService,
    private location: Location,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
  }

  importPlaylist() {
    this.array = [this.path];
    this.importationService.addPlaylistImportation(this.selectedType, this.array).subscribe(
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
