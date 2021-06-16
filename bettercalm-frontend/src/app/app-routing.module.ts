import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddAdministratorFormComponent } from './components/add-administrator-form/add-administrator-form.component';
import { AddPlayablecontentFormComponent } from './components/add-playablecontent-form/add-playablecontent-form.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { CategoryElementListComponent } from './components/category-element-list/category-element-list.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { AdministratorManagementListComponent } from './components/administrator-management-list/administrator-management-list.component';
import { AdministratorEditionFormComponent } from './components/administrator-edition-form/administrator-edition-form.component';
import { PlayablecontentManagementListComponent } from './components/playablecontent-management-list/playablecontent-management-list.component';
import { AddPlaylistFormComponent } from './components/add-playlist-form/add-playlist-form.component';
import { PlaylistManagementListComponent } from './components/playlist-management-list/playlist-management-list.component';
import { PlaylistContentManagementListComponent } from './components/playlist-content-management-list/playlist-content-management-list.component';
import { AddVideoFormComponent } from './components/add-video-form/add-video-form.component';
import { VideoManagementListComponent } from './components/video-management-list/video-management-list.component';
import { AddPsychologistFormComponent } from './components/add-psychologist-form/add-psychologist-form.component';
import { PsychologistManagementListComponent } from './components/psychologist-management-list/psychologist-management-list.component';
import { PsychologistEditionFormComponent } from './components/psychologist-edition-form/psychologist-edition-form.component';
import { AddConsultationFormComponent } from './components/add-consultation-form/add-consultation-form.component';
import { PsychologistScheduleEditionFormComponent } from './components/psychologist-schedule-edition-form/psychologist-schedule-edition-form.component';
import { ConsultationManagementListComponent } from './components/consultation-management-list/consultation-management-list.component';
import { ImportPlayablecontentFormComponent } from './components/import-playablecontent-form/import-playablecontent-form.component';
import { ImportPlaylistFormComponent } from './components/import-playlist-form/import-playlist-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/categories', pathMatch: 'full' },
  { path: 'categories', component: CategoryListComponent },
  { path: 'categories/:id', component: CategoryElementListComponent },
  { path: 'administrators', component: AdministratorManagementListComponent },
  { path: 'administrators/edit/:id', component: AdministratorEditionFormComponent },
  { path: 'administrators/add', component: AddAdministratorFormComponent },
  { path: 'playablecontents', component: PlayablecontentManagementListComponent },
  { path: 'playablecontents/add', component: AddPlayablecontentFormComponent },
  { path: 'videos', component: VideoManagementListComponent },
  { path: 'videos/add', component: AddVideoFormComponent },
  { path: 'playlists', component: PlaylistManagementListComponent },
  { path: 'playlists/add', component: AddPlaylistFormComponent },
  { path: 'playlists/contents/:id', component: PlaylistContentManagementListComponent },
  { path: 'login', component: LoginFormComponent },
  { path: 'psychologists', component: PsychologistManagementListComponent },
  { path: 'psychologists/add', component: AddPsychologistFormComponent },
  { path: 'psychologists/edit/:id', component: PsychologistEditionFormComponent },
  { path: 'consultations/add', component: AddConsultationFormComponent },
  { path: 'psychologists/editschedule/:id', component: PsychologistScheduleEditionFormComponent },
  { path: 'consultations', component: ConsultationManagementListComponent },
  { path: 'playablecontents/import', component: ImportPlayablecontentFormComponent },
  { path: 'playlists/import', component: ImportPlaylistFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
