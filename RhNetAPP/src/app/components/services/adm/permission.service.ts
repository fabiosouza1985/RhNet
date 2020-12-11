import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Permission } from 'src/app/components/models/adm/permission.model';
import { Constants } from 'src/app/components/constants';

@Injectable({
    providedIn: 'root'
})
export class PermissionService {

    constructor(private http: HttpClient, private constants: Constants) { }

    getAll(): Observable<Permission[]> {
        return this.http.get<Permission[]>(this.constants.Url + 'permission/getAll');
    };

    get(): Observable<Permission[]> {
        return this.http.get<Permission[]>(this.constants.Url + 'permission/get');
    };


    add(permission: Permission): Observable<Permission> {
        return this.http.post<Permission>(this.constants.Url + 'permission/add', permission);
    }

    update(permission: Permission): Observable<Permission> {
        return this.http.post<Permission>(this.constants.Url + 'permission/update', permission);
    }

    remove(permission: Permission): Observable<Permission> {
        return this.http.post<Permission>(this.constants.Url + 'permission/remove', permission);
    }
}