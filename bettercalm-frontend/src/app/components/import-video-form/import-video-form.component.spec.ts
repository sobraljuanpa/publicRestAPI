import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportVideoFormComponent } from './import-video-form.component';

describe('ImportVideoFormComponent', () => {
  let component: ImportVideoFormComponent;
  let fixture: ComponentFixture<ImportVideoFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportVideoFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportVideoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
