import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddAdministratorFormComponent } from './components/add-administrator-form/add-administrator-form.component';
import { AddPlayablecontentFormComponent } from './components/add-playablecontent-form/add-playablecontent-form.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { CategoryElementListComponent } from './components/category-element-list/category-element-list.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { AdministratorManagementListComponent } from './components/administrator-management-list/administrator-management-list.component';
import { AdministratorEditionFormComponent } from './components/administrator-edition-form/administrator-edition-form.component';
import { PlayablecontentEditionFormComponent } from './components/playablecontent-edition-form/playablecontent-edition-form.component';
import { PlayablecontentManagementListComponent } from './components/playablecontent-management-list/playablecontent-management-list.component';
import { AddPlaylistFormComponent } from './components/add-playlist-form/add-playlist-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/categories', pathMatch: 'full'},
  { path: 'categories', component: CategoryListComponent },
  { path: 'categories/:id', component: CategoryElementListComponent },
  { path: 'administrators', component: AdministratorManagementListComponent},
  { path: 'administrators/edit/:id', component: AdministratorEditionFormComponent},
  { path: 'administrators/add', component: AddAdministratorFormComponent},
  { path: 'playablecontents', component: PlayablecontentManagementListComponent},
  { path: 'playablecontents/edit/:id', component: PlayablecontentEditionFormComponent},
  { path: 'playablecontents/add', component: AddPlayablecontentFormComponent},
  { path: 'playlists/add', component: AddPlaylistFormComponent},
  { path: 'login', component: LoginFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
