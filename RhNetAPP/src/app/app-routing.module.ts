import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {HomeComponent} from './components/views/home/home.component';
import {LoginComponent} from './components/views/login/login.component';
import {ProfileComponent} from './components/views/adm/profile/profile.component';
import {ViewMenusComponent} from './components/views/adm/view-menus/view-menus.component';
import { NotFoundComponent } from './components/views/adm/not-found/not-found.component';
import { ViewProfilesComponent } from './components/views/adm/view-profiles/view-profiles.component';

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
        path: "**",
        component: NotFoundComponent
    }
    
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
