import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { HeaderComponent } from './components/template/header/header.component';

import {MatToolbarModule } from '@angular/material/toolbar';
import { FooterComponent } from './components/template/footer/footer.component';
import { NavComponent } from './components/template/nav/nav.component';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';
import {MatIconModule} from '@angular/material/icon';
import {MatGridListModule} from '@angular/material/grid-list';
import {FlexLayoutModule} from '@angular/flex-layout';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatRippleModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { HomeComponent } from './components/views/home/home.component';
import { LoginComponent } from './components/views/login/login.component';

import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './components/services/adm/token.interceptor';
import { ProfileComponent } from './components/views/adm/profile/profile.component';
import { ViewMenusComponent } from './components/views/adm/view-menus/view-menus.component';
import { MenuItemComponent } from './components/template/menu-item/menu-item.component';
import { NotFoundComponent } from './components/views/adm/not-found/not-found.component';
import { ViewProfilesComponent } from './components/views/adm/view-profiles/view-profiles.component';
import { ViewPermissionsComponent } from './components/views/adm/view-permissions/view-permissions.component';
import { ViewClientsComponent } from './components/views/adm/clients/view-clients/view-clients.component';
import { ViewUsersComponent } from './components/views/adm/users/view-users/view-users.component';
import { AddUserComponent } from './components/views/adm/users/add-user/add-user.component';
import { SelectClientComponent } from './components/views/adm/clients/select-client/select-client.component';
import { MunicipiosComponent } from './components/views/shared/municipios/municipios.component';
import { EntidadesComponent } from './components/views/shared/entidades/entidades.component';
import { TiposDeAtoNormativoComponent } from './components/views/shared/tipos-de-ato-normativo/tipos-de-ato-normativo.component';
import { AtosNormativosComponent } from './components/views/shared/atos-normativos/atos-normativos.component';
import { HeaderPageComponent } from './components/template/header-page/header-page.component';
import { EditableTableComponent } from './components/template/editable-table/editable-table.component';
import { QuadrosComponent } from './components/views/shared/quadros/quadros.component';
import { SubquadrosComponent } from './components/views/shared/subquadros/subquadros.component';
import { UpdateUserComponent } from './components/views/adm/users/update-user/update-user.component';
import { QuadroDetalheComponent } from './components/views/shared/quadros/quadro-detalhe/quadro-detalhe.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavComponent,
    HomeComponent,
    LoginComponent,
    ProfileComponent,
    ViewMenusComponent,
    MenuItemComponent,
    NotFoundComponent,
    ViewProfilesComponent,
    ViewPermissionsComponent,
    ViewClientsComponent,
    ViewUsersComponent,
    AddUserComponent,
    SelectClientComponent,
    MunicipiosComponent,
    EntidadesComponent,
    TiposDeAtoNormativoComponent,
    AtosNormativosComponent,
    HeaderPageComponent,
    EditableTableComponent,
    QuadrosComponent,
    SubquadrosComponent,
    UpdateUserComponent,
    QuadroDetalheComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
      BrowserAnimationsModule,
      FormsModule,
      ReactiveFormsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    HttpClientModule,
    MatMenuModule,
    MatIconModule,
    MatGridListModule,
    FlexLayoutModule,
      MatTableModule,
      MatSelectModule,
      MatRippleModule,
      MatCheckboxModule,
      MatSnackBarModule,
      MatSortModule,
      MatPaginatorModule,
      MatProgressSpinnerModule,
      MatRadioModule,
      MatTabsModule,
      MatSlideToggleModule,
      MatProgressBarModule,
      MatDatepickerModule,
      MatNativeDateModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
        multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
