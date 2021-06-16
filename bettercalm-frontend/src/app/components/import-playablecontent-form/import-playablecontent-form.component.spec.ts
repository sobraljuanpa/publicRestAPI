import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportPlayablecontentFormComponent } from './import-playablecontent-form.component';

describe('ImportPlayablecontentFormComponent', () => {
  let component: ImportPlayablecontentFormComponent;
  let fixture: ComponentFixture<ImportPlayablecontentFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportPlayablecontentFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportPlayablecontentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
