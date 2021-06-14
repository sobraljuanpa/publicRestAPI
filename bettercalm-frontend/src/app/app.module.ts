import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TokenInterceptor } from "./services/token.interceptor";
import { NavbarComponent } from './components/navbar/navbar.component';
import { CategoryComponent } from './components/category/category.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { AddAdministratorFormComponent } from './components/add-administrator-form/add-administrator-form.component';
import { AddPlayablecontentFormComponent } from './components/add-playablecontent-form/add-playablecontent-form.component';
import { CategoryElementComponent } from './components/category-element/category-element.component';
import { CategoryElementListComponent } from './components/category-element-list/category-element-list.component';
import { AdministratorManagementListComponent } from './components/administrator-management-list/administrator-management-list.component';
import { AdministratorEditionFormComponent } from './components/administrator-edition-form/administrator-edition-form.component';
import { PlayablecontentManagementListComponent } from './components/playablecontent-management-list/playablecontent-management-list.component';
import { AddPsychologistFormComponent } from './components/add-psychologist-form/add-psychologist-form.component';
import { AddPlaylistFormComponent } from './components/add-playlist-form/add-playlist-form.component';
import { PlaylistManagementListComponent } from './components/playlist-management-list/playlist-management-list.component';
import { PlaylistContentManagementListComponent } from './components/playlist-content-management-list/playlist-content-management-list.component';
import { AddVideoFormComponent } from './components/add-video-form/add-video-form.component';
import { VideoManagementListComponent } from './components/video-management-list/video-management-list.component';
import { VideoListElementComponent } from './components/video-list-element/video-list-element.component';
import { PsychologistManagementListComponent } from './components/psychologist-management-list/psychologist-management-list.component';
import { PsychologistEditionFormComponent } from './components/psychologist-edition-form/psychologist-edition-form.component';
import { AddConsultationFormComponent } from './components/add-consultation-form/add-consultation-form.component';
import { PsychologistScheduleEditionFormComponent } from './components/psychologist-schedule-edition-form/psychologist-schedule-edition-form.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryComponent,
    CategoryListComponent,
    LoginFormComponent,
    AddAdministratorFormComponent,
    AddPlayablecontentFormComponent,
    CategoryElementComponent,
    CategoryElementListComponent,
    AdministratorManagementListComponent,
    AdministratorEditionFormComponent,
    PlayablecontentManagementListComponent,
    AddPsychologistFormComponent,
    AddPlaylistFormComponent,
    PlaylistManagementListComponent,
    PlaylistContentManagementListComponent,
    AddVideoFormComponent,
    VideoManagementListComponent,
    VideoListElementComponent,
    PsychologistManagementListComponent,
    PsychologistEditionFormComponent,
    AddConsultationFormComponent,
    PsychologistScheduleEditionFormComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
