import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { CategoryElement } from 'src/app/models/categoryElement';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category-element-list',
  templateUrl: './category-element-list.component.html',
  styleUrls: ['./category-element-list.component.css']
})
export class CategoryElementListComponent implements OnInit {

  categoryElements!: CategoryElement[];

  constructor(private route: ActivatedRoute, private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getCategoryElements();
  }

  getCategoryElements() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.categoryService.getCategoryElements(id)
    .subscribe(categoryElements => this.categoryElements = categoryElements)
  }
}
