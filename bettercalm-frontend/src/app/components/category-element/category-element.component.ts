import { Component, Input, OnInit, SecurityContext } from '@angular/core';
import { CategoryElement } from 'src/app/models/categoryElement';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser'

@Component({
  selector: 'app-category-element',
  templateUrl: './category-element.component.html',
  styleUrls: ['./category-element.component.css']
})
export class CategoryElementComponent implements OnInit {

  @Input() categoryElement?: CategoryElement;
  videoURL!: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    if(this.categoryElement?.videoURL != undefined) {
      this.videoURL = this.sanitizer.bypassSecurityTrustResourceUrl(this.categoryElement?.videoURL!!)!!;
    }
  }

}
