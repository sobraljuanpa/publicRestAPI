import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistManagementListComponent } from './playlist-management-list.component';

describe('PlaylistManagementListComponent', () => {
  let component: PlaylistManagementListComponent;
  let fixture: ComponentFixture<PlaylistManagementListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlaylistManagementListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaylistManagementListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
