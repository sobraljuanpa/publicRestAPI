import { Component, Input, OnInit } from '@angular/core';
import { Category } from "../../models/category";

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  @Input() category?: Category;

  constructor() { }

  ngOnInit(): void {
  }

}
