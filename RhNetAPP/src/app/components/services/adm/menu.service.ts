import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Menu } from 'src/app/components/models/adm/menu.model';
import { Constants } from 'src/app/components/constants';

@Injectable({
    providedIn: 'root'
})
export class MenuService {

    constructor(private http: HttpClient, private constants: Constants) { }    

    getMenus(profile): Observable<Menu[]> {
        let httpParams = new HttpParams().set('profile', profile)
        return this.http.get<Menu[]>(this.constants.Url + 'menu/getMenus', { params: httpParams });
    };

    getQuickAccess(profile): Observable<Menu[]> {
        let httpParams = new HttpParams().set('profile', profile)
        return this.http.get<Menu[]>(this.constants.Url + 'menu/getQuickAccess', { params: httpParams });
    };
    getAllMenus(): Observable<Menu[]> {
        return this.http.get<Menu[]>(this.constants.Url + 'menu/getallmenus');
    };

    addMenu(menu: Menu): Observable<Menu> {
        return this.http.post<Menu>(this.constants.Url + 'menu/addMenu', menu);
    }

    updateMenu(menu: Menu): Observable<Menu> {
        return this.http.post<Menu>(this.constants.Url + 'menu/updateMenu', menu);
    }

    removeMenu(menu: Menu): Observable<Menu> {
        return this.http.post<Menu>(this.constants.Url + 'menu/removeMenu', menu);
    }
}