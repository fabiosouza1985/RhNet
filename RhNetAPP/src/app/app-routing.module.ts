import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {HomeComponent} from './components/views/home/home.component';
import {LoginComponent} from './components/views/login/login.component';
import {ProfileComponent} from './components/views/adm/profile/profile.component';
import {ViewMenusComponent} from './components/views/adm/view-menus/view-menus.component';
import { NotFoundComponent } from './components/views/adm/not-found/not-found.component';
import { ViewProfilesComponent } from './components/views/adm/view-profiles/view-profiles.component';
import { ViewPermissionsComponent } from './components/views/adm/view-permissions/view-permissions.component';
import { ViewClientsComponent } from './components/views/adm/clients/view-clients/view-clients.component';
import { ViewUsersComponent } from './components/views/adm/users/view-users/view-users.component';
import { AddUserComponent } from './components/views/adm/users/add-user/add-user.component';
import { SelectClientComponent } from './components/views/adm/clients/select-client/select-client.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
      path: "login",
      component: LoginComponent
  }
  ,
  {
      path: "myprofile",
      component: ProfileComponent
  }
  ,
  {
      path: "menus",
      component: ViewMenusComponent
    }
    ,
    {
        path: "profiles",
        component: ViewProfilesComponent
    },
    {
        path: "permissions",
        component: ViewPermissionsComponent
    },
    {
        path: "clients",
        component: ViewClientsComponent
    },
    {
        path: "users",
        component: ViewUsersComponent
    },
    {
        path: "users/adduser",
        component: AddUserComponent
    },
    {
        path: "selectClient",
        component: SelectClientComponent
    },
    {
        path: "**",
        component: NotFoundComponent
    }
    
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
