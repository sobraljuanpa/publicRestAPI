import { Component, Input, OnInit } from '@angular/core';
import { CategoryElement } from 'src/app/models/categoryElement';

@Component({
  selector: 'app-category-element',
  templateUrl: './category-element.component.html',
  styleUrls: ['./category-element.component.css']
})
export class CategoryElementComponent implements OnInit {

  @Input() categoryElement?: CategoryElement;

  constructor() { }

  ngOnInit(): void {
    console.log(this.categoryElement);
  }

}
