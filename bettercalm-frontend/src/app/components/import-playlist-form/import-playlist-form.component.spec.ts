import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportPlaylistFormComponent } from './import-playlist-form.component';

describe('ImportPlaylistFormComponent', () => {
  let component: ImportPlaylistFormComponent;
  let fixture: ComponentFixture<ImportPlaylistFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportPlaylistFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportPlaylistFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
