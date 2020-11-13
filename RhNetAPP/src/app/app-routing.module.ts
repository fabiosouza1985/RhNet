import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {HomeComponent} from './components/views/home/home.component';
import {LoginComponent} from './components/views/login/login.component';
import {ProfileComponent} from './components/views/adm/profile/profile.component';
import {ViewMenusComponent} from './components/views/adm/view-menus/view-menus.component';
import { NotFoundComponent } from './components/views/adm/not-found/not-found.component';
import { QuickAccessComponent } from './components/views/adm/quick-access/quick-access.component';

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
      path: "profile",
      component: ProfileComponent
  }
  ,
  {
      path: "menus",
      component: ViewMenusComponent
    },
    {
        path: "quick_access",
        component: QuickAccessComponent
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
